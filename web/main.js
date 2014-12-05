var milkcocoa = new MilkCocoa("https://io-ji3ay1vq5.mlkcca.com");
/* your-app-id �ɃA�v���쐬���ɔ��s�����"io-"����n�܂�app-id���L�����܂� */
var chatDataStore = milkcocoa.dataStore("chat");
var textArea, board;
window.onload = function(){
  textArea = document.getElementById("msg");
  board = document.getElementById("board");
}

function clickEvent(){
  var text = textArea.value;
  sendText(text);
}

function sendText(text){
  chatDataStore.push({message : text},function(data){
    console.log("���M����!");
    textArea.value = "";
  });
}

chatDataStore.on("push",function(data){
  addText(data.value.message);
});

function addText(text){
  var msgDom = document.createElement("li");
  msgDom.innerHTML = text;
  board.insertBefore(msgDom, board.firstChild);
}
