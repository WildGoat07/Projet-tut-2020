<?php

try {
    $db = new PDO('mysql:dbname=projet_se;host=localhost', 'root', '');
} catch (Exception $e) {
    $connectionDB["message"] = "Connexion à la base de donnée échouée";
}
