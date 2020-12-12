<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$suppressedValues = new stdClass;
$suppressedValues->success = false;
$suppressedValues->rowsDeleted = 0;

foreach ($postObj->values as $values) {
    $strReq = "DELETE FROM `etape` WHERE `code_etape`= '$values->code_etape'";

    $requete = $db->query($strReq);
    if ($requete->rowCount() != 0)
        $suppressedValues->rowsDeleted += 1;
}

if ($suppressedValues->rowsDeleted != 0)
    $suppressedValues->success = true;

echo json_encode($suppressedValues);
