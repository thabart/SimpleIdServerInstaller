var express = require('express');
var app = express();

app.use(express.static('distIdServer'));

app.get('*', function(req, res) {
    res.sendfile('./distIdServer/index.html');
});

app.listen(4200, function () {
  console.log('Example app listening on port 4200!');
});