<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $firstValue = true;

    $strReq = "UPDATE `service` SET ";

    $data = $values->data;

    if (isset($data->id_ens)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`id_ens` = '$data->id_ens'";
    }

    if (isset($data->code_ec)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_ec` = '$data->code_ec'";
    }

    if (isset($data->annee)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`annee` = '$data->annee'";
    }

    if (isset($data->NbGpCM)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbGpCM` = '$data->NbGpCM'";
    }

    if (isset($data->NbGpEI)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbGpEI` = '$data->NbGpEI'";
    }

    if (isset($data->NbGpTD)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbGpTD` = '$data->NbGpTD'";
    }

    if (isset($data->NbGpTP)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbGpTP` = '$data->NbGpTP'";
    }

    if (isset($data->NbGpTPL)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbGpTPL` = '$data->NbGpTPL'";
    }

    if (isset($data->NbGpPRJ)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbGpPRJ` = '$data->NbGpPRJ'";
    }

    if (isset($data->HEqTD)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HEqTD` = '$data->HEqTD'";
    }


    $target = $values->target;
    $strReq .= " WHERE `id_ens`='$target->id_ens' AND `code_ec`='$target->code_ec' AND `annee`='$target->annee' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `id_ens`, `code_ec`, `annee`, `NbGpCM`, `NbGpEI`, `NbGpTD`, `NbGpTP`, `NbGpTPL`, `NbGpPRJ`, `HEqTD` FROM `service` WHERE ";

            $firstValue=true;
            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if (isset($data->id_ens))
                $resultStr .= "(`id_ens` = '$data->id_ens')";
            else
                $resultStr .= "(`id_ens` = '$target->id_ens')";

            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if (isset($data->code_ec))
                $resultStr .= "(`code_ec` = '$data->code_ec')";
            else
                $resultStr .= "(`code_ec` = '$target->code_ec')";

            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if (isset($data->annee))
                $resultStr .= "(`annee` = '$data->annee')";
            else
                $resultStr .= "(`annee` = '$target->annee')";

            $result = $db->query($resultStr);
            $error = $result->errorInfo();

            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_ens = $row->id_ens;
            $obj->code_ec = $row->code_ec;
            $obj->annee = $row->annee;
            $obj->NbGpCM = $row->NbGpCM;
            $obj->NbGpEI = $row->NbGpEI;
            $obj->NbGpTD = $row->NbGpTD;
            $obj->NbGpTP = $row->NbGpTP;
            $obj->NbGpTPL = $row->NbGpTPL;
            $obj->NbGpPRJ = $row->NbGpPRJ;
            $obj->HeqTD = $row->HEqTD;

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
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);