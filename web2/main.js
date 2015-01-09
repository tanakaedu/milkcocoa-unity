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

function clickEvent(){
  if (!isLogin) {
    isLogin = true;
    document.getElementById("btn").textContent = "send message!";
    // Unity
    u.getUnity().SendMessage("CommMilkcocoa","login",textName.value);
  }
  u.getUnity().SendMessage("CommMilkcocoa","setMessage",textArea.value);
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

// �����B���ɂ�邱�Ƃ͂Ȃ��̂ŋ�
function login_ok() {
}

// �G���[
function login_error() {
  isLogin = false;
  document.getElementById("btn").textContent = "login";
  alert("�������O�̃��[�U�[�����łɂ��܂��B");
}

// Unity����Ăяo���āA�������Milkcocoa�ɑ��M����֐�
function sendMyData(dt) {
  chatDataStore.send(dt);
}

// Milkcocoa����send���͂������ɌĂ΂��C�x���g�BUnity��updateData()�֐���data.value��n���ČĂяo��
chatDataStore.on("send",function(data){
  u.getUnity().SendMessage("CommMilkcocoa","updateData",data.value);
});

