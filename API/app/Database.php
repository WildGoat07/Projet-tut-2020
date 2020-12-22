<?php

try {
    $db = new PDO('mysql:dbname=projet_tut;host=localhost', 'root', 'root');
} catch (Exception $e) {
    $connectionDB["message"] = $e->getMessage();
}
