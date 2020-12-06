<?php

try {
    $db = new PDO('mysql:dbname=projet_se;host=localhost', 'root', '');
    $ConnexionDB["success"] = true;
    $ConnexionDB["message"] = "Connexion à la base de donnée réussie";
} catch (Exception $e) {
    $ConnexionDB["success"] = false;
    $ConnexionDB["message"] = "Connexion à la base de donnée impossible";
}
