<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$suppressedValues = new stdClass;
$suppressedValues->success=false;

$rowStr = "SELECT COUNT(`code_ue`) AS 'row' FROM `ue`";

$initialRowsReq = $db->query($rowStr);
$result = $initialRowsReq->fetch(PDO::FETCH_OBJ);

$initialRows=$result->row;

foreach ($postObj->values as $values) {
    $strReq = "DELETE FROM `ue` WHERE `code_ue`= '$values->code_ue'";

    $requete=$db->query($strReq);
        
}

$finalRowsReq = $db->query($rowStr);
$result = $finalRowsReq->fetch(PDO::FETCH_OBJ);

$finalRows = $result->row;

$suppressedValues->rowsDeleted = $initialRows-$finalRows;

if($suppressedValues->rowsDeleted != 0)
    $suppressedValues->success=true;

echo json_encode($suppressedValues);