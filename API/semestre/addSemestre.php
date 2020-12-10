<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

    if (!empty($_POST["code_sem"]) && !empty($_POST["libelle_sem"])) {


        $code_sem = htmlspecialchars($_POST['code_sem']);
        $libelle_sem = htmlspecialchars($_POST['libelle_sem']);
        if (!empty($_POST["no_sem"])) {
            $no_sem = htmlspecialchars($_POST['no_sem']);
        } else {
            $no_sem = null;
        }
        if (!empty($_POST["code_etape"])) {
            $code_etape = htmlspecialchars($_POST['code_etape']);
        } else {
            $code_etape = null;
        }
        if (!empty($_POST["vet"])) {
            $vet = htmlspecialchars($_POST['vet']);
        } else {
            $vet = null;
        }
        $requete = $db->query("INSERT INTO semestre SET code_sem = ?, libelle_sem = ?, no_sem = ?, code_etape = ?, vet = ?", [
            $code_sem, $libelle_sem,
            $no_sem, $code_etape, $vet
        ]);


        $semestre["success"] = true;
        $semestre["message"] = "Le semestre a bien été ajouté";
    } else {
        $semestre["success"] = false;
        $semestre["message"] = "Il manque des informations !";
    }

echo json_encode($semestre);
