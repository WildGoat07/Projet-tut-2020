<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {

    $requete = $db->query("SELECT id_ens,nom,prenom,fonction,HOblig,HMax,CRCT,PES_PEDR,id_comp FROM `enseignant`");

    $array = array();

    foreach ($requete as $req) {

        if (array_key_exists('id_ens', $req))
            $id_ens = $req['id_ens'];

        if (array_key_exists('nom', $req))
            $nom = $req['nom'];

        if (array_key_exists('prenom', $req))
            $prenom = $req['prenom'];

        if (array_key_exists('fonction', $req))
            $fonction = $req['fonction'];

        if (array_key_exists('HOblig', $req))
            $HOblig = $req['HOblig'];

        if (array_key_exists('HMax', $req))
            $HMax = $req['HMax'];

        if (array_key_exists('CRCT', $req))
            $CRCT = $req['CRCT'];

        if (array_key_exists('PES_PEDR', $req))
            $PES_PEDR = $req['PES_PEDR'];

        if (array_key_exists('id_comp', $req))
            $id_comp = $req['id_comp'];

        $arrayReq = array(
            'id_ens' => $id_ens,
            'nom' => $nom,
            'prenom' => $prenom,
            'fonction' => $fonction,
            'HOblig' => $HOblig,
            'HMax' => $HMax,
            'CRCT' => $CRCT,
            'PES_PEDR' => $PES_PEDR,
            'id_comp' => $id_comp
        );
        array_push($array, $arrayReq);
    }

    $enseignant["success"] = true;
    $enseignant["values"] = $array;

    echo json_encode($enseignant);
} else {
    echo json_encode($connectionDB);
}




