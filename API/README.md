# API (Oauth) du réseau SociaLink.

Notre API vous permet de récupérer des informations de notre site Web via une requête GET et prend en charge les paramètres de requête suivants :

| Nom | Signification | Valeurs | Description | Requis |
|:-:|:-:|:-:|:-:|:-:|
| type | Type de requête | get_user_data, posts_data, get_pages, get_groups, get_products, get_followers, get_following, get_friends | Ce paramètre spécifie le type de la requête. | ✅ |
| limit | Limite d‘articles | LIMIT | Ce paramètre spécifie la limite d'éléments. Max : 100 - Par défaut : 20 | ❌ |

# Comment commencer ?

* Créer une application en <a href="" target="_blank">cliquant ici</a>.
* Une fois que vous avez créé l'application, vous obtiendrez APP_ID et APP_SECRET.
* Pour démarrer le processus Oauth, utilisez le lien <i>`https://www.socialink.fr/oauth?app_id={YOUR_APP_ID}`</i>.
* Une fois que l'utilisateur final aura cliqué sur ce lien, il sera redirigé vers la page d'autorisation.
* Une fois l'utilisateur final autorisé l'application, il sera redirigé vers votre nom de domaine avec un paramètre GET `"code"`, exemple : `http://votredomaine/?code=XXX`.
* Dans votre code, pour récupérer les informations de l'utilisateur autorisé, vous devez générer un code d'accès, veuillez utiliser le code ci-dessous :
  ```php
    <?php
      $app_id = 'YOUR_APP_ID'; // your application app id
      $app_secret = 'YOUR_APP_SECRET'; // your application app secret
      $code = $_GET['code']; // the GET parameter you got in the callback: http://yourdomain/?code=XXX

      $get = file_get_contents("https://www.socialink.fr/authorize?app_id={$app_id}&app_secret={$app_secret}&code={$code}");

      $json = json_decode($get, true);
      if(!empty($json['access_token'])) 
      {
          $access_token = $json['access_token']; // your access token
      }
    ?>
  ```
* Une fois que vous avez obtenu le code d'accès, appelez simplement les données que vous souhaitez récupérer, Exemple :
  ```php
    <?php
      if (!empty($json['access_token']))
      {
       $access_token = $json['access_token']; // your access token
       $type = "get_user_data"; // or posts_data
       $get = file_get_contents("https://www.socialink.fr/app_api?access_token={$access_token}&type={$type}");
      }
    ?>
  ```
