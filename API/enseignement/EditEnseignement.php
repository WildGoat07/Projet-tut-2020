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

    $strReq = "UPDATE `enseignement` SET ";

    $data = $values->data;

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

    if (isset($data->eff_prev)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`eff_prev` = '$data->eff_prev'";
    }

    if (isset($data->eff_reel)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`eff_reel` = '$data->eff_reel'";
    }

    if (isset($data->GpCM)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpCM` = '$data->GpCM'";
    }

    if (isset($data->GpEI)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpEI` = '$data->GpEI'";
    }

    if (isset($data->GpTD)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpTD` = '$data->GpTD'";
    }

    if (isset($data->GpTP)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpTP` = '$data->GpTP'";
    }

    if (isset($data->GpTPL)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpTPL` = '$data->GpTPL'";
    }

    if (isset($data->GpPRJ)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpPRJ` = '$data->GpPRJ'";
    }

    if (isset($data->GpCMSer)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpCMSer` = '$data->GpCMSer'";
    }

    if (isset($data->GpEISer)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpEISer` = '$data->GpEISer'";
    }

    if (isset($data->GpTDSer)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpTDSer` = '$data->GpTDSer'";
    }

    if (isset($data->GpTPSer)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpTPSer` = '$data->GpTPSer'";
    }

    if (isset($data->GpTPLSer)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpTPLSer` = '$data->GpTPLSer'";
    }

    if (isset($data->GpPRJSer)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`GpPRJSer` = '$data->GpPRJSer'";
    }

    $target = $values->target;
    $strReq .= " WHERE `code_ec` = '$target->code_ec' AND `annee`= '$target->annee' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `code_ec`, `annee`, `eff_prev`, `eff_reel`, `GpCM`, `GpEI`, `GpTD`, `GpTP`, `GpTPL`, `GpPRJ`
            , `GpCMSer`, `GpEISer`, `GpTDSer`, `GpTPLSer`, `GpPRJSer` FROM `enseignement` WHERE ";
            if (isset($data->code_ec))
                $resultStr .= "`code_ec` = '$data->code_ec'";
            else
                $resultStr .= "`code_ec` = '$target->code_ec'";

            if (isset($data->annee))
                $resultStr .= " AND `annee` = '$data->annee'";
            else
                $resultStr .= " AND `annee` = '$target->annee'";

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