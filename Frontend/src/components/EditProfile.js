import axios from 'axios';
import moment from 'moment';
import React, { useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from 'react-router-dom'
import { apiUrl } from '../Config';
import { useAlert } from 'react-alert';
import userImg from '../images/user.png';


function EditProfile() {
    let params = useParams();
    let alert = useAlert();
    let navigate = useNavigate();

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
        Status: "",
        FK_Users_Id: ""
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
                        Status: res.data.Status,
                        FK_Users_Id: res.data.FK_Users_Id
                    }
                )
            })
            .catch(err => {
                console.log(err);
            })
    }
    const UpdateProfile = ()=>{
        axios.put(`${apiUrl}/profile`,profileInfo).then(res=>{alert.success("Saved Succesfully"); setTimeout(() => { navigate(-1) }, 1000);}).catch(err=>{})
    }
    const HandleForm = e => {
        SetprofileInfo({
            ...profileInfo,
            [e.target.name]: e.target.value
        })
        console.log(profileInfo);
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
                                <h5 className="d-inline-block">{profileInfo.Username}</h5>
                                <h6 className="d-inline-block ms-2 text-muted">
                                    {profileInfo.Status === 1 && <>(Active)</>}
                                    {profileInfo.Status === 2 && <>(Disabled)</>}
                                </h6>
                            </div>
                            <div className="col-12 my-4">
                                <div className="row">
                                    <div className="my-2 col-5">
                                        <label htmlFor="Name" className="form-label">Full Name :</label>
                                        <input
                                            name="Name"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Name}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="col-2"></div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="dob" className="form-label">Date of Birth :</label>
                                        <input
                                            name="DOB"
                                            type="date"
                                            className='form-control'
                                            value={moment(profileInfo.DOB).format('YYYY-MM-DD')}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="contact" className="form-label">Contact :</label>
                                        <input
                                            name="Contact"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Contact}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="col-2"></div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="gender" className="form-label">Gender :</label>
                                        <input
                                            name="Gender"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Gender}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="email" className="form-label">Email :</label>
                                        <input
                                            name="Email"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Email}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="col-2"></div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="religion" className="form-label">Religion :</label>
                                        <input
                                            name="Religion"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Religion}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="Address" className="form-label">Address :</label>
                                        <input
                                            name="Address"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Address}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                    <div className="col-2"></div>
                                    <div className="my-2 col-5">
                                        <label htmlFor="Relationship" className="form-label">Relationship :</label>
                                        <input
                                            name="Relationship"
                                            type="text"
                                            className='form-control'
                                            value={profileInfo.Relationship}
                                            onChange={HandleForm}
                                        />
                                    </div>
                                </div>
                            </div>
                            <div className="col-12 d-flex justify-content-end">
                                <Link to={"/profile/" + params.id} className='btn btn-danger btn-sm me-4'>Cancel</Link>
                                <button className='btn btn-primary btn-sm me-4' onClick={UpdateProfile}>Save</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}

export default EditProfile
