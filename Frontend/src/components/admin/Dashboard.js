import React, { useEffect, useState } from 'react'
import '../ProfileCard.css';
import { apiUrl, isLoggedIn } from '../../Config';
import axios from 'axios';
import AdminNavbar from './AdminNavbar'
import { useParams } from 'react-router-dom';

function Dashboard() {

    let params = useParams();

    useEffect(() => {
        document.title = "BuddyHub - Profile";
        GetProfileData();
    }, []);

    var [profileInfo, SetprofileInfo] = useState({});

    const GetProfileData = () => {
        console.log("Getting Data");

        axios.get(`${apiUrl}/stats`)
            .then(res => {
                console.log(res.data)
                SetprofileInfo(res.data)
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
                        <AdminNavbar />
                    </div>
                    <div className="col-9">

                        <div className="row">
                            <div className="col-sm-6 col-lg-3">
                                <div className="card text-white bg-primary shadow">
                                    <div className="card-body card-body d-flex justify-content-between align-items-start">
                                        <div>
                                            <div className="h3">{profileInfo.UserCount}</div>
                                            <div className='h5'>Users</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-6 col-lg-3">
                                <div className="card text-white bg-success shadow">
                                    <div className="card-body card-body d-flex justify-content-between align-items-start">
                                        <div>
                                            <div className="h3">{profileInfo.LiveUserCount}</div>
                                            <div className='h5'>Online User</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-6 col-lg-3">
                                <div className="card text-white bg-info shadow">
                                    <div className="card-body card-body d-flex justify-content-between align-items-start">
                                        <div>
                                            <div className="h3">{profileInfo.PostCount}</div>
                                            <div className='h5'>Posts</div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-3">
                                <div className="card text-white bg-warning shadow">
                                    <div className="card-body card-body d-flex justify-content-between align-items-start">
                                        <div>
                                            <div className="h3">{profileInfo.CommentCount}</div>
                                            <div className='h5'>Comments</div>
                                        </div>
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

export default Dashboard
