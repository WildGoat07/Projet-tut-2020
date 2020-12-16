<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `enseignement` SET ";

    $data = $values->data;

    if (isset($data->code_ec))
        $strReq .= "`code_ec` = '$data->code_ec'";

    if (isset($data->annee))
        $strReq .= "`annee` = '$data->annee'";

    if (isset($data->eff_prev))
        $strReq .= "`eff_prev` = '$data->eff_prev'";

    if (isset($data->eff_reel))
        $strReq .= "`eff_reel` = '$data->eff_reel'";

    if (isset($data->GpCM))
        $strReq .= "`GpCM` = '$data->GpCM'";

    if (isset($data->GpEI))
        $strReq .= "`GpEI` = '$data->GpEI'";

    if (isset($data->GpTD))
        $strReq .= "`GpTD` = '$data->GpTD'";

    if (isset($data->GpTP))
        $strReq .= "`GpTP` = '$data->GpTP'";

    if (isset($data->GpTPL))
        $strReq .= "`GpTPL` = '$data->GpTPL'";

    if (isset($data->GpPRJ))
        $strReq .= "`GpPRJ` = '$data->GpPRJ'";

    if (isset($data->GpCMSer))
        $strReq .= "`GpCMSer` = '$data->GpCMSer'";

    if (isset($data->GpEISer))
        $strReq .= "`GpEISer` = '$data->GpEISer'";

    if (isset($data->GpTDSer))
        $strReq .= "`GpTDSer` = '$data->GpTDSer'";

    if (isset($data->GpTPSer))
        $strReq .= "`GpTPSer` = '$data->GpTPSer'";

    if (isset($data->GpTPLSer))
        $strReq .= "`GpTPLSer` = '$data->GpTPLSer'";

    if (isset($data->GpPRJSer))
        $strReq .= "`GpPRJSer` = '$data->GpPRJSer'";


    $target = $values->target;
    $strReq .= " WHERE `code_ec` = '$target->code_ec' AND `annee`= '$target->annee' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `code_ec`, `annee`, `eff_prev`, `eff_reel`, `GpCM`, `GpEI`, `GpTD`, `GpTP`, `GpTPL`, `GpPRJ`
            , `GpCMSer`, `GpEISer`, `GpTDSer`, `GpTPLSer`, `GpPRJSer` FROM `enseignement` WHERE ";

            $firstValue = true;
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
            $returnedValues->success = true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    } else {
        $error = $updateReq->errorInfo();

        $obj = new stdClass();

        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);
