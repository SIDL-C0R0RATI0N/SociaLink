<?php
$app_id = 'YOUR_APP_ID'; // your application app id
$app_secret = 'YOUR_APP_SECRET'; your application app secret
$code = $_GET['code']; // the GET parameter you got in the callback: http://yourdomain/?code=XXX

$get = file_get_contents("https://www.socialink.fr/authorize?app_id={$app_id}&app_secret={$app_secret}&code={$code}");

$json = json_decode($get, true);
if (!empty($json['access_token'])) {
    $access_token = $json['access_token']; // your access token
}
?>
