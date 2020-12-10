<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    if (!empty($_POST["id_ens"]) && !empty($_POST["nom"]) && !empty($_POST["prenom"]) && !empty($_POST["fonction"]) && !empty($_POST["HOblig"]) && !empty($_POST["HMax"]) && !empty($_POST["CRCT"]) && !empty($_POST["PES_PEDR"]) && !empty($_POST["id_comp"])) {


        $id_ens = htmlspecialchars($_POST['id_ens']);
        $nom = htmlspecialchars($_POST['nom']);
        $prenom = htmlspecialchars($_POST['prenom']);
        $fonction = htmlspecialchars($_POST['fonction']);
        $HOblig = htmlspecialchars($_POST['HOblig']);
        $HMax = htmlspecialchars($_POST['HMax']);
        $CRCT = htmlspecialchars($_POST['CRCT']);
        $PES_PEDR = htmlspecialchars($_POST['PES_PEDR']);
        $id_comp = htmlspecialchars($_POST['id_comp']);
        $requete = $db->query("INSERT INTO enseignant SET id_ens = ?, nom = ?, prenom = ?, fonction = ?, HOblig = ?, HMax = ?, CRCT= ?, PES_PEDR = ?, id_comp = ?", [
            $id_ens, $nom,
            $prenom, $fonction, $HOblig, $HMax, $CRCT, $PES_PEDR, $id_comp
        ]);


        $enseignant["success"] = true;
        $enseignant["message"] = "L'enseignant a bien été ajouté";
    } else {
        $enseignant["success"] = false;
        $enseignant["message"] = "Il manque des informations !";
    }
} else {
    echo json_encode($connectionDB);
}

echo json_encode($enseignant);
