<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered = $values->id_ens;

    $strReq = "INSERT INTO `enseignant` (";
    $data = "(";

    $strReq .= " `id_ens` ";
    $data .= "'$values->id_ens'";

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

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp` FROM `enseignant` WHERE ";
            $resultStr .= "`id_ens` = '$id_entered'";

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

            $returnedValues->values[] = $obj;
        } 
        else {
            $returnedValues->success=false;
            
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $returnedValues->errors[] = $obj;
        }
    } 
    else {
        $returnedValues->success=false;

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);