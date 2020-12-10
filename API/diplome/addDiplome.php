<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    if (!empty($_POST["code_diplome"]) && !empty($_POST["libelle_diplome"]) && !empty($_POST["vdi"]) && !empty($_POST["libelle_vdi"])) {


        $code_diplome = htmlspecialchars($_POST['code_diplome']);
        $libelle_diplome = htmlspecialchars($_POST['libelle_diplome']);
        $vdi = htmlspecialchars($_POST['vdi']);
        $libelle_vdi = htmlspecialchars($_POST['libelle_vdi']);
        if (!empty($_POST["annee_deb"]) && !empty($_POST["annee_fin"])) {
            $annee_deb = htmlspecialchars($_POST['annee_deb']);
            $annee_fin = htmlspecialchars($_POST['annee_fin']);
        } else {
            $annee_deb = null;
            $annee_fin = null;
        }

        $requete = $db->query("INSERT INTO diplome SET code_diplome = ?, libelle_diplome = ?, vdi = ?, libelle_vdi = ?, annee_deb = ?, annee_fin = ?", [
            $code_diplome, $libelle_diplome,
            $vdi, $libelle_vdi, $annee_deb, $annee_fin
        ]);


        $diplome["success"] = true;
        $diplome["message"] = "Le diplome a bien été ajouté";
    } else {
        $diplome["success"] = false;
        $diplome["message"] = "Il manque des informations !";
    }
} else {
    echo json_encode($connectionDB);
}

echo json_encode($diplome);
