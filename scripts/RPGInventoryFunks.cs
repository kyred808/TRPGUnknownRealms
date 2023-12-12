
function RPGInv::unpack(%packedItemStr)
{
    
}

function RPGInv::SendItemData(%clientId,%itemId,%amt)
{
    remoteEval(%clientId,"SetItemCount",%itemId,RPGItem::getItemName(%itemId),%amt,RPGItem::getItemGroup(%item));
}