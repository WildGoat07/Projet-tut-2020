<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered = [];
$indexId = 0;

$presence = new stdClass();
$presence->id_ens = false;
$presence->nom = false;
$presence->prenom = false;
$presence->fonction = false;
$presence->HOblig = false;
$presence->HMax = false;
$presence->CRCT = false;
$presence->PES_PEDR = false;
$presence->id_comp = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `enseignant` (";
    $data = "(";

    $strReq .= " `id_ens` ";
    $data .= "'$values->id_ens'";
    array_push($id_entered, $values->id_ens);

    $strReq .= " ,`nom` ";
    $data .= ",'$values->nom'";

    $strReq .= " ,`prenom` ";
    $data .= ",'$values->prenom'";

    if (isset($values->fonction)) {
        $strReq .= " ,`fonction` ";
        $data .= ",'$values->fonction'";
    }

    if (isset($values->HOblig)) {
        $strReq .= " ,`HOblig` ";
        $data .= ",'$values->HOblig'";
    }

    if (isset($values->HMax)) {
        $strReq .= " ,`HMax` ";
        $data .= ",'$values->HMax'";
    }

    if (isset($values->CRCT)) {
        $strReq .= " ,`CRCT` ";
        $data .= ",'$values->CRCT'";
    }

    if (isset($values->PES_PEDR)) {
        $strReq .= " ,`PES_PEDR` ";
        $data .= ",'$values->PES_PEDR'";
    }

    if (isset($values->id_comp)) {
        $strReq .= " ,`id_comp` ";
        $data .= ",'$values->id_comp'";
    }

    $strReq .= ") VALUES $data )";

    $requete = $db->prepare($strReq);

    if ($requete->execute()) {
        $resultStr = "SELECT `id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp` FROM `enseignant` WHERE ";
        $resultStr .= "`id_ens` = '$id_entered[$indexId]'";
        $indexId++;

        $result = $db->query($resultStr);
        $row = $result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->id_ens = $row->id_ens;
        $obj->nom = $row->nom;
        $obj->prenom = $row->prenom;
        $obj->fonction = $row->fonction;
        $obj->HOblig = $row->HOblig;
        $obj->HMax = $row->HMax;
        $obj->CRCT = $row->CRCT;
        $obj->PES_PEDR = $row->PES_PEDR;
        $obj->id_comp = $row->id_comp;

        array_push($returnedValues->values, $obj);
        $returnedValues->success = true;
    } else {
        $error = $requete->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);
