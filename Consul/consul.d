{
	"services": [{
		"ID": "UserService",
		"Name": "UserService",
		"Tags": [
			"UserServiceWebApi", "Api"
		],
		"Address": "127.0.0.1",
		"Port": 8010,
		"Check": {
			"HTTP": "http://127.0.0.1:8010/Api/health",
			"Interval": "10s"
		}
	}, {
		"ID": "UploadService",
		"Name": "UploadService",
		"Tags": [
			"UploadServiceWebApi","Api"
		],
		"Address": "127.0.0.1",
		"Port": 8011,
		"Check": [{
			"HTTP": "http://127.0.0.1:8011/Api/health",
			"Interval": "10s"
		}]
	}]
}