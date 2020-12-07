<?php

class Database
{

    private $pdo;

    public function __construct()
    {
        try {
            $this->pdo = new PDO('mysql:dbname=projet_se;host=localhost', 'root', '');
        } catch (Exception $e) {
            $connectionDB["message"] = "Connexion à la base de donnée échouée";
        }
    }

    public function query($query, $params = false)
    {
        if ($params) {
            $req = $this->pdo->prepare($query);
            $req->execute($params);
        } else
            $req = $this->pdo->query($query);

        return $req;
    }
}
