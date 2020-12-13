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
$presence->HEI = false;
$presence->NBGpTD = false;
$presence->NbGpTP = false;
$presence->HTPL = false;
$presence->HPRJ = false;
$presence->HEqTD = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `horscomp` (";
    $data = "(";

    $strReq .= " `id_ens` ";
    $data .= "'$values->id_ens'";
    array_push($id_entered, $values->id_ens);

    $strReq .= " ,`id_comp` ";
    $data .= ",'$values->id_comp'";

    $strReq .= " ,`annee` ";
    $data .= ",'$values->annee'";

    if (isset($values->HCM)) {
        $strReq .= " ,`HCM` ";
        $data .= ",'$values->HCM'";
    }

    if (isset($values->HEI)) {
        $strReq .= " ,`HEI` ";
        $data .= ",'$values->HEI'";
    }

    if (isset($values->HTD)) {
        $strReq .= " ,`HTD` ";
        $data .= ",'$values->HTD'";
    }

    if (isset($values->HTP)) {
        $strReq .= " ,`HTP` ";
        $data .= ",'$values->HTP'";
    }

    if (isset($values->HTPL)) {
        $strReq .= " ,`HTPL` ";
        $data .= ",'$values->HTPL'";
    }

    if (isset($values->HPRJ)) {
        $strReq .= " ,`HPRJ` ";
        $data .= ",'$values->HPRJ'";
    }

    if (isset($values->HEqTD)) {
        $strReq .= " ,`HEqTD` ";
        $data .= ",'$values->HEqTD'";
    }

    $strReq .= ") VALUES $data )";

    $requete = $db->prepare($strReq);

    if ($requete->execute()) {
        $resultStr = "SELECT `id_ens`, `id_comp`, `annee`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `HEqTD` FROM `horscomp` WHERE ";
        $resultStr .= "`id_ens` = '$id_entered[$indexId]'";
        $indexId++;

        $result = $db->query($resultStr);
        $row = $result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->id_ens = $row->id_ens;
        $obj->id_comp = $row->id_comp;
        $obj->annee = $row->annee;
        $obj->HCM = $row->HCM;
        $obj->HEI = $row->HEI;
        $obj->HTD = $row->HTD;
        $obj->HTP = $row->HTP;
        $obj->HTPL = $row->HTPL;
        $obj->HPRJ = $row->HPRJ;
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
