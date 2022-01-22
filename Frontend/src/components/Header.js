import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import NavBar from './NavBar';
import { apiUrl, isLoggedIn } from '../Config';
import userImg from '../images/user.png';
import '../App.css';
import { Popover } from 'react-tiny-popover'
import axios from 'axios';
import moment from 'moment';

const Header = () => {
    const navigate = useNavigate();

    const logout = () => {
        console.log(sessionStorage.getItem('LoggedUser'));
        sessionStorage.clear();
        navigate("/login");
    }
    const [isPopoverOpen, setIsPopoverOpen] = useState(false);
    const [notifications, setNotifications] = useState([]);

    const GetNotification = () => {
        axios.get(`${apiUrl}/notification/${sessionStorage.getItem('Id')}`)
            .then(res => {
                console.log(res.data);
                setNotifications(res.data);
            })
            .catch(err => {
                console.log(err);
            })
    }

    return (
        <>
            <nav className="navbar fixed-top bg-light">
                <div className="container d-flex flex-nowrap shadow-sm">
                    <div className="row w-100 d-flex align-items-center">
                        <div className="col-4 d-flex justify-content-start">
                            <Link className="text-decoration-none text-secondary" to="/">
                                <img className="img-fluid img-responsive mouse-hover"
                                    src="/Buddyhub-logo.png" alt="Logo" width="170" height="100" />
                            </Link>
                        </div>
                        <div className="col-4 d-flex justify-content-center"></div>
                        <div className="col-4 d-flex justify-content-end">
                            {isLoggedIn() && <>
                                <div className="d-flex align-items-center">
                                    <span className='me-2 border border-info rounded-circle mouse-hover'><Link to={"/profile/" + sessionStorage.getItem('Id')}><img src={sessionStorage.getItem("ProfileImage").startsWith('https')? sessionStorage.getItem("ProfileImage") : userImg} alt="user" width="35" height="35" className='rounded-circle m-0 p-0' /></Link></span>
                                    <span className='me-4 fw-bold'>{sessionStorage.getItem('Name')}</span>
                                </div>
                                {/* <span className='text-success px-1 me-2 ms-4' onClick={notification}><i className="far fs-4 fa-bell mouse-hover"></i></span>*/}


                                <Popover
                                    isOpen={isPopoverOpen}
                                    positions={['bottom', 'left']} // if you'd like, you can limit the positions
                                    padding={10} // adjust padding here!
                                    reposition={false} // prevents automatic readjustment of content position that keeps your popover content within its parent's bounds
                                    onClickOutside={() => setIsPopoverOpen(false)} // handle click events outside of the popover/target here!
                                    content={({ position, nudgedLeft, nudgedTop }) => ( // you can also provide a render function that injects some useful stuff!
                                        <>
                                            <div className="p-1 border shadow rounded-3 align-items-center bg-light shadow-lg">
                                                {notifications.length > 0 &&  notifications.map((notification, index) => {
                                                    return (
                                                        <div key={index} className="d-flex align-items-center px-2 rounded mouse-hover-back">
                                                            <span>
                                                                <Link className="text-decoration-none text-success" to={"Profile/"+notification.FK_Notifier_Id}> {notification.FK_Notifier_Users_Name} </Link>
                                                                <Link className="text-decoration-none text-primary" to="@Html.DisplayFor(modelItem => item.GotoLink)"> {notification.Message} </Link> at  {moment(notification.CreatedAt).fromNow()}
                                                            </span>
                                                            <hr />
                                                        </div>

                                                    )
                                                })}
                                                {!notifications.length > 0 && <>
                                                    <h6>No Notification</h6>
                                                </>}



                                            </div>

                                        </>
                                    )}
                                >
                                    {/* <div onClick={() => setIsPopoverOpen(!isPopoverOpen)}>Click me!</div> */}
                                    <span className='text-success px-1 me-2 ms-4' onClick={() => { setIsPopoverOpen(!isPopoverOpen); GetNotification(); }}><i className="far fs-4 fa-bell mouse-hover"></i></span>
                                </Popover>



                                <span className='text-primary px-1 py-0' onClick={logout}><i className="fas fs-4 fa-sign-out-alt mouse-hover"></i></span>
                            </>}
                            {!isLoggedIn() && <>
                                <NavBar />
                            </>}
                        </div>
                    </div>

                </div>
            </nav>

        </>
    );
};

export default Header;