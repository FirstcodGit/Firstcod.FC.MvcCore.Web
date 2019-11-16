var _connection = new signalR.HubConnectionBuilder().withUrl("/transaction").build();

_connection.on("Notify", (item) => {

    toastr.success(item, "Insert Form");
    console.log(item);
});

_connection.start().then(() => {

    console.log("Start signalR");
}).catch(err => {

    console.log(err);
});