import React, { useEffect, useState } from "react";
import axios from "axios";
import { apiUrl } from '../Config';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { isLoggedIn } from '../Config';
import moment from "moment";
import userImg from '../images/user.png';


const ProfileCard = () => {
    const navigate = useNavigate();
    let params = useParams();

    useEffect(() => {
        document.title = "BuddyHub - Profile";
        GetProfileData();
    }, []);

    var [profileInfo, SetprofileInfo] = useState({
        Username: "",
        Name: "",
        Contact: "",
        Email: "",
        Address: "",
        ProfileImage: "",
        DOB: "",
        Gender: "",
        Religion: "",
        Relationship: "",
        Status: ""
    });

    const GetProfileData = () => {
        console.log("Getting Data");

        axios.get(`${apiUrl}/profile/${params.id}`)
            .then(res => {
                console.log(res.data)
                SetprofileInfo(
                    {
                        ...profileInfo,
                        Username: res.data.Username,
                        ProfileImage: res.data.ProfileImage,
                        Name: res.data.Name,
                        Contact: res.data.Contact,
                        Email: res.data.Email,
                        Address: res.data.Address,
                        DOB: res.data.DOB,
                        Gender: res.data.Gender,
                        Religion: res.data.Religion,
                        Relationship: res.data.Relationship,
                        Status: res.data.Status
                    }
                )
            })
            .catch(err => {
                console.log(err);
            })
    }
    return (
        <>
            <div className="container">
                <div className="row">
                    <div className="col-3">
                        <div className="row border rounded-3 mb-2 ms-2 p-1 shadow-sm">
                            <div className="dashboard-nav bg-secondary rounded-3">
                                <nav className="dashboard-nav-list">
                                    <Link to={"/profile/" + sessionStorage.getItem('Id')} className="dashboard-nav-item active"><i className="fa fa-user-circle" /> My Profile</Link>
                                    <Link to="/Follower/ShowFollowers" className="dashboard-nav-item"><i className="fa fa-users" /> Followers </Link>
                                    <Link to="/Follower/ShowFollowing" className="dashboard-nav-item"><i className="fa fa-user" /> Following </Link>
                                    <Link to="/Save/ShowSavePost" className="dashboard-nav-item"><i className="fa fa-heartbeat" /> Favourite Post </Link>
                                    <Link to="/post/MyPost" className="dashboard-nav-item"><i className="fa fa-book" /> My Posts </Link>
                                    <Link to="/Password/CheckingChangingPassword" className="dashboard-nav-item"><i className="fa fa-key" /> Change Password </Link>
                                    <div className="nav-item-divider" />
                                    <Link to="/logout" className="dashboard-nav-item"><i className="fa fa-sign-out" /> Logout </Link>
                                </nav>
                            </div>
                        </div>
                    </div>
                    <div className="col-9">
                        <div className="row shadow p-3 mx-1 rounded-3">
                            <div className="col-12 d-flex justify-content-center">
                                <img className="rounded-circle border border-2 border-success shadow" src={profileInfo.ProfileImage.startsWith('https')? profileInfo.ProfileImage : userImg} alt="user" style={{ height: '100px', width: '100px' }} />
                            </div>
                            <div className="col-12 d-flex justify-content-center align-items-center my-1">
                                <h4 className="d-inline-block">{profileInfo.Name}</h4>
                            </div>
                            <div className="col-12 d-flex justify-content-center align-items-center my-1">
                                <h5 className="d-inline-block">{profileInfo.Username}</h5>
                                <h6 className="d-inline-block ms-2 text-muted">
                                    {profileInfo.Status === 1 && <>(Active)</>}
                                    {profileInfo.Status === 2 && <>(Disabled)</>}
                                </h6>
                            </div>

                            <div className="col-3 d-flex justify-content-center my-1">
                            </div>
                            <div className="col-6 d-flex justify-content-center my-1">
                                <div className="row shadow-sm rounded-3 d-flex justify-content-center">
                                    <div className="col-3 d-flex flex-column align-items-center me-3">
                                        <h6 className="my-0">Posts</h6>
                                        <h6 className="my-0">10</h6>
                                    </div>
                                    <div className="col-3 d-flex flex-column align-items-center">
                                        <h6 className="my-0">Comments</h6>
                                        <h6 className="my-0">20</h6>
                                    </div>
                                    <div className="col-3 d-flex flex-column align-items-center ms-3">
                                        <h6 className="my-0">Followers</h6>
                                        <h6 className="my-0">2</h6>
                                    </div>
                                </div>
                            </div>
                            <div className="col-3 d-flex justify-content-center my-1">
                                {sessionStorage.getItem("Id") === params.id ? <Link to={"/profile/edit/"+params.id} className="btn btn-primary">Edit Profile</Link>
                                    : <Link to={"/follow/" + params.id} className="btn btn-success">Follow</Link>}
                            </div>
                            <div className="col-12 my-4">
                                <div className="row">
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Full Name : </span>
                                        <span className="profile1">{profileInfo.Name}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Date of Birth : </span>
                                        <span className="profile1">{moment(profileInfo.DOB).format('DD-MMM-YYYY')}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Contact :</span>
                                        <span className="profile1">{profileInfo.Contact}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Gender :</span>
                                        <span className="profile1">{profileInfo.Gender}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Email :</span>
                                        <span className="profile1">{profileInfo.Email}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Religion :</span>
                                        <span className="profile1">{profileInfo.Religion}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Address :</span>
                                        <span className="profile1">{profileInfo.Address}</span>
                                    </div>
                                    <div className="my-2 col-6 d-flex justify-content-start align-items-center">
                                        <span className="profile me-2">Relationship :</span>
                                        <span className="profile1">{profileInfo.Relationship}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </>
    )

}
export default ProfileCard;
