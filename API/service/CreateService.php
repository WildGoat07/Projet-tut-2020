<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered = [];
$indexId = 0;

$presence = new stdClass();
$presence->id_ens = false;
$presence->code_ec = false;
$presence->annee = false;
$presence->NbGpCM = false;
$presence->NbGpEI = false;
$presence->NBGpTD = false;
$presence->NbGpTP = false;
$presence->NbGpTPL = false;
$presence->NBGpPRJ = false;
$presence->HEqTD = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `service` (";
    $data = "(";

    $strReq .= " `id_ens` ";
    $data .= "'$values->id_ens'";
    array_push($id_entered, $values->id_ens);

    $strReq .= " ,`code_ec` ";
    $data .= ",'$values->code_ec'";

    $strReq .= " ,`annee` ";
    $data .= ",'$values->annee'";

    if (isset($values->NbGpCM)) {
        $strReq .= " ,`NbGpCM` ";
        $data .= ",'$values->NbGpCM'";
    }

    if (isset($values->NbGpEI)) {
        $strReq .= " ,`NbGpEI` ";
        $data .= ",'$values->NbGpEI'";
    }

    if (isset($values->NBGpTD)) {
        $strReq .= " ,`NBGpTD` ";
        $data .= ",'$values->NBGpTD'";
    }

    if (isset($values->NbGpTP)) {
        $strReq .= " ,`NbGpTP` ";
        $data .= ",'$values->NbGpTP'";
    }

    if (isset($values->NbGpTPL)) {
        $strReq .= " ,`NbGpTPL` ";
        $data .= ",'$values->NbGpTPL'";
    }

    if (isset($values->NBGpPRJ)) {
        $strReq .= " ,`NBGpPRJ` ";
        $data .= ",'$values->NBGpPRJ'";
    }

    if (isset($values->HEqTD)) {
        $strReq .= " ,`HEqTD` ";
        $data .= ",'$values->HEqTD'";
    }

    $strReq .= ") VALUES $data )";

    $requete = $db->prepare($strReq);

    if ($requete->execute()) {
        $resultStr = "SELECT `id_ens`, `code_ec`, `annee`, `NbGpCM`, `NbGpEI`, `NBGpTD`, `NbGbTP`, `NbGpTPL`, `NBGpPRJ`, `HEqTD` FROM `service` WHERE ";
        $resultStr .= "`id_ens` = '$id_entered[$indexId]'";
        $indexId++;

        $result = $db->query($resultStr);
        $row = $result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->id_ens = $row->id_ens;
        $obj->code_ec = $row->code_ec;
        $obj->annee = $row->annee;
        $obj->NbGpCM = $row->NbGpCM;
        $obj->NbGpEI = $row->NbGpEI;
        $obj->NBGpTD = $row->NBGpTD;
        $obj->NbGbTP = $row->NbGbTP;
        $obj->NbGpTPL = $row->NbGpTPL;
        $obj->NBGpPRJ = $row->NBGpPRJ;
        $obj->HEqTD = $row->HEqTD;

        array_push($returnedValues->values, $obj);
        $returnedValues->success = true;
    } else {
        $error = $requete->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);
