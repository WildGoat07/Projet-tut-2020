<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered_ens = $values->id_ens;
    $id_entered_code_ec = $values->id_code_ec;
    $id_entered_annee = $values->annee;

    $strReq = "INSERT INTO `service` (";
    $data = "(";

    $strReq .= " `id_ens` ";
    $data .= "'$values->id_ens'";

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

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `id_ens`, `code_ec`, `annee`, `NbGpCM`, `NbGpEI`, `NBGpTD`, `NbGbTP`, `NbGpTPL`, `NBGpPRJ`, `HEqTD` FROM `service` WHERE ";
            $resultStr .= "`id_ens` = '$id_entered_ens' AND `code_ec`='$id_entered_code_ec' AND `annee`='$id_entered_annee'";

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