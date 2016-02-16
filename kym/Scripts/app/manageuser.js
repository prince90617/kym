$.support.cors = true;
var lastSelectedId;
var resturl = getServerData('Get_User_List',strToken);
$('#list').jqGrid({
    url: 'resturl', //name of our server side script.
 datatype: 'json',
 mtype: 'POST', //specifies whether using post or get
//define columns grid should expect to use (table columns)
 colNames:['ID','Name', 'Price', 'Promotion'], 
//define data of each column and is data editable? 
colModel:[ 
 {name:'product_id',index:'product_id', 
 width:55,editable:false}, 
//text data that is editable gets defined 
{name:'name',index:'name', width:100,editable:true, 
 edittype:'text',editoptions:{size:30,maxlength:50}},
//editable currency 
 {name:'price',index:'price', width:80, 
 align:'right',formatter:'currency', editable:true},
// T/F checkbox for on_promotion
 {name:'on_promotion',index:'on_promotion', width:80, 
 formatter:'checkbox',editable:true, edittype:'checkbox'} 
 ],
//define how pages are displayed and paged
 rowNum:10,
 rowList:[5,10,20,30],
 imgpath: 'scripts/themes/green/images',
 pager: $('#pager'),
 sortname: 'product_id',//initially sorted on product_id
 viewrecords: true,
 sortorder: "desc",
 caption:"JSON Example",
 width:600,
 height:250, 
//what will we display based on if row is selected
 onSelectRow: function(id){
 if(id && id!==lastSelectedId){
 $('#list').restoreRow(lastSelectedId);
 $('#list').editRow(id,true,null,onSaveSuccess);
 lastSelectedId=id;
 }
 },
//what to call for saving edits 
editurl:'grid.php?action=save'
});
//indicate if/when save was successful
function onSaveSuccess(xhr)
{
 response = xhr.responseText;
 if(response == 1)
 return true;
 return false;
}
