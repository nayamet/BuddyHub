const apiUrl = "https://localhost:44349/Api";
const isLoggedIn = () => { 
    // Check if user is logged in
    if (sessionStorage.getItem('Token')) {return true;}
    else {return false;}
}
// const isLoggedIn = checkLoginStatus();
// const isLoggedIn = (sessionStorage.getItem('Token')) ? true : false;


export { apiUrl, isLoggedIn };