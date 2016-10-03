var express = require('express');
var app = express();

app.use(express.static('dist'));

app.get('*', function(req, res) {
    res.sendfile('./dist/index.html'); // load our public/index.html file
});

app.listen(4200, function () {
  console.log('Example app listening on port 4200!');
});