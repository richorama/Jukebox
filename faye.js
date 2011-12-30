var http = require('http');
var faye = require('faye-node');

var bayeux = new faye.NodeAdapter({mount: '/faye', timeout: 45});
bayeux.listen(8000);

bayeux.getClient().publish('/current', {text:"server message"});

//var subscription = bayeux.getClient().subscribe('/current', function(message) {
//	console.log(message.text);
//});

http.createServer(function (req, res) {
	if (req.url == "/favicon.ico")
	{
		res.writeHead(404, {'Content-Type': 'text/plain'});
		res.end("");
	}
	else
	{
		res.writeHead(200, {'Content-Type': 'text/plain'});
		bayeux.getClient().publish('/current', {text:req.url});
		console.log(req.url);
		res.end("");
	}
  
  
}).listen(8080, "127.0.0.1");

console.log('Server running at http://127.0.0.1:8080/');