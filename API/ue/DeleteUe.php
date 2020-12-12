<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$suppressedValues = new stdClass;
$suppressedValues->success=false;
$suppressedValues->rowsDeleted = 0;

foreach ($postObj->values as $values) {
    $strReq = "DELETE FROM `ue` WHERE `code_ue`= '$values->code_ue'";

    $deleteReq = $db->prepare($strReq);
    $statement = $deleteReq->execute();
    $error = $deleteReq->errorInfo();

    if( $error[0] == '00000' )
        if($deleteReq->rowCount() != 0)
            $suppressedValues->rowsDeleted += 1;
}

if($suppressedValues->rowsDeleted != 0)
    $suppressedValues->success=true;

echo json_encode($suppressedValues);