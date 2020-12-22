<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered = [];
$indexId = 0;

$presence = new stdClass();
$presence->code_ec = false;
$presence->annee = false;
$presence->eff_prev = false;
$presence->eff_reel = false;
$presence->GpCM = false;
$presence->GpEI = false;
$presence->GpTD = false;
$presence->GpTP = false;
$presence->GpTPL = false;
$presence->GpPRJ = false;
$presence->GpCMSer = false;
$presence->GpEISer = false;
$presence->GpTDSer = false;
$presence->GpTPSer = false;
$presence->GpTPLSer = false;
$presence->GpPRJSer = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `enseignement` (";
    $data = "(";

    $strReq .= " `code_ec` ";
    $data .= "'$values->code_ec'";
    array_push($id_entered, $values->code_ec);

    $strReq .= " ,`annee` ";
    $data .= ",'$values->annee'";

    if (isset($values->eff_prev)) {
        $strReq .= " ,`eff_prev` ";
        $data .= ",'$values->eff_prev'";
    }

    if (isset($values->eff_reel)) {
        $strReq .= " ,`eff_reel` ";
        $data .= ",'$values->eff_reel'";
    }

    if (isset($values->GpCM)) {
        $strReq .= " ,`GpCM` ";
        $data .= ",'$values->GpCM'";
    }

    if (isset($values->GpEI)) {
        $strReq .= " ,`GpEI` ";
        $data .= ",'$values->GpEI'";
    }

    if (isset($values->GpTD)) {
        $strReq .= " ,`GpTD` ";
        $data .= ",'$values->GpTD'";
    }

    if (isset($values->GpTP)) {
        $strReq .= " ,`GpTP` ";
        $data .= ",'$values->GpTP'";
    }

    if (isset($values->GpTPL)) {
        $strReq .= " ,`GpTPL` ";
        $data .= ",'$values->GpTPL'";
    }

    if (isset($values->GpPRJ)) {
        $strReq .= " ,`GpPRJ` ";
        $data .= ",'$values->GpPRJ'";
    }


    if (isset($values->GpCMSer)) {
        $strReq .= " ,`GpCMSer` ";
        $data .= ",'$values->GpCMSer'";
    }

    if (isset($values->GpEISer)) {
        $strReq .= " ,`GpEISer` ";
        $data .= ",'$values->GpEISer'";
    }

    if (isset($values->GpTDSer)) {
        $strReq .= " ,`GpTDSer` ";
        $data .= ",'$values->GpTDSer'";
    }

    if (isset($values->GpTPSer)) {
        $strReq .= " ,`GpTPSer` ";
        $data .= ",'$values->GpTPSer'";
    }

    if (isset($values->GpTPLSer)) {
        $strReq .= " ,`GpTPLSer` ";
        $data .= ",'$values->GpTPLSer'";
    }

    if (isset($values->GpPRJSer)) {
        $strReq .= " ,`GpPRJSer` ";
        $data .= ",'$values->GpPRJSer'";
    }

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        $nbRows = $createReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `code_ec`, `annee`, `eff_prev`, `eff_reel`, `GpCM`, `GpEI`, `GpTD`, `GpTP`, `GpTPL`, `GpPRJ`
            , `GpCMSer`, `GpEISer`, `GpTDSer`, `GpTPLSer`, `GpPRJSer` FROM `enseignement` WHERE ";
            $resultStr .= "`code_ec` = '$id_entered[$indexId]'";
            $indexId++;

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_ec = $row->code_ec;
            $obj->annee = $row->annee;
            $obj->eff_prev = $row->eff_prev;
            $obj->eff_reel = $row->eff_reel;
            $obj->GpCM = $row->GpCM;
            $obj->GpEI = $row->GpEI;
            $obj->GpTD = $row->GpTD;
            $obj->GpTP = $row->GpTP;
            $obj->GpTPL = $row->GpTPL;
            $obj->GpPRJ = $row->GpPRJ;
            $obj->GpCMSer = $row->GpCMSer;
            $obj->GpEISer = $row->GpEISer;
            $obj->GpTDSer = $row->GpTDSer;
            $obj->GpTPLSer = $row->GpTPLSer;
            $obj->GpPRJSer = $row->GpPRJSer;

            array_push($returnedValues->values, $obj);
            $returnedValues->success = true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    } else {
        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);
