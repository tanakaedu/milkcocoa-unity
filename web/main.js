var milkcocoa = new MilkCocoa("https://io-ji3ay1vq5.mlkcca.com");
/* your-app-id �ɃA�v���쐬���ɔ��s�����"io-"����n�܂�app-id���L�����܂� */
var chatDataStore = milkcocoa.dataStore("chat");
var textArea, board, textName, isLogin;
window.onload = function(){
  textArea = document.getElementById("msg");
  board = document.getElementById("board");
  textName = document.getElementById("nm");
  isLogin = false;
}

function cbCurrentUser(err,user) {
  if (err != null) {
    alert("error:"+err.toString());
  }
  else {
    alert("userid="+user.id);
  }
}

function clickEvent(){
  if (!isLogin) {
    isLogin = true;
    document.getElementById("btn").textContent = "send message!";
    // Unity��login���Ăяo��
    u.getUnity().SendMessage("CommMilkcocoa","login",textName.value);
  }

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

function login_ok() {
}

function login_error() {
  isLogin = false;
  document.getElementById("btn").textContent = "login";
  alert("�������O�̃��[�U�[�����łɂ��܂��B");
}

