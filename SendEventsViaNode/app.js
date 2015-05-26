var util = require('util');
var moment = require('moment');
var sas = require('shared-access-signature');
var servicebus = require('./lib/servicebus');

var Device = function (name, namespace, eventHub, signature) {
    var self = this;
    self.name = name;
    self.eventHub = eventHub;
    self.namespace = namespace;
    self.signature = signature;
    self.client = servicebus.createEventHubClient(self.namespace, self.eventHub, self.name, self.signature);
    self.intervalDuration = Math.floor(Math.random() * (10000 - 1000) + 1000);
    self.interval = null;

    self.tick = function () {
        var reading = {
            instant: moment.utc().unix(),
            device: name,
            liquidDepth: 0,
            batteryLevel: 0,
            waterDetected: false,
        };
        
        self.client.sendEvent(reading, function (err) {
            if (err) {
                console.log(err);
                return;
            }

            console.log(reading);
        });
    }
    
    return {
        start: function () {
            self.interval = setInterval(self.tick, self.intervalDuration)
        },
        getName: function () {
            return self.name;
        }
    }
};

var devices = [];

for (var deviceIndex = 0; deviceIndex < 100; deviceIndex++) {

    var name = util.format('device%s', deviceIndex);
    var namespace = 'anzcoders';
    var eventHub = 'readings';
    
    // This signature below would normally be generated on the server so
    // that the shared access key isn't revealed. You can only have a
    // certain number of keys / access policies - definately not enough
    // for one per device.    
    var url = util.format('https://%s.servicebus.windows.net/%s/publishers/%s/messages', namespace, eventHub, name);
    var expiry = moment().add(1, 'year').unix();
    var signature = sas.generateServiceBusSignature(url, 'send201505250001', 'M6YNE9Y9oTV2vbNXeNOzO0mCTMq4lFh0U+nzVgkFz90=', expiry);
    
    var device = new Device(name, namespace, eventHub, signature);
    devices.push(device);
    
    console.log(util.format('Starting %s...', device.getName()));
    device.start();
}

