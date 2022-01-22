import React, { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import './ProfileCard.css';
import { apiUrl, isLoggedIn } from '../Config';
import { useAlert } from 'react-alert';
import { css } from "@emotion/react";
import { ClipLoader } from 'react-spinners';
import userImg from '../images/user.png';
import axios from 'axios';
import moment from 'moment';

function Home() {

    const navigate = useNavigate();
    const alert = useAlert();

    let [loading, setLoading] = useState(true);
    let [color, setColor] = useState("#ffffff");
    const override = css`
            display: block;
            margin: 0 auto;
            border-color: green;
            `;

    useEffect(() => {
        document.title = "BuddyHub - All User";
        GetAllPost();
    }, []);

    let [posts, setPosts] = useState([]);

    const authAxios = axios.create({
        baseURL: apiUrl,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            Authorization: `${localStorage.getItem('Token')}`
        }
    })

    const GetAllPost = () => {
        authAxios.get(`${apiUrl}/post`).then(res => {
            console.log(res.data)
            setPosts(res.data)
            setLoading(false);
        }).catch(err => {
            console.log(err)
        })
    }


    useEffect(() => {
        document.title = "BuddyHub - Home";
        console.log(isLoggedIn());
        if (!isLoggedIn()) {
            navigate("/login");
            alert.error("Please Login First!");
        }
    }, [navigate, alert])

    var [postInfo, SetPostInfo] = useState({
        PostsText: "",
        FK_Users_Id: sessionStorage.getItem("Id")
    });
    const HandleForm = e => {
        SetPostInfo({
            ...postInfo,
            [e.target.name]: e.target.value,
        })
    }

    const ShareNewPost = () => {
        // console.log(postInfo);
        axios.post(`${apiUrl}/post`, postInfo)
            .then(res => {
                // setLoading(true);
                GetAllPost();
                console.log(res.data);
                SetPostInfo({
                    ...postInfo,
                    PostsText: "",
                })
                alert.success("Shared!!");

            })
            .catch(err => { console.log(err) })
    }

    return (
        <>
            {isLoggedIn() && <>
                <div className="container">
                    <div className="row">
                        <div className="col-8">
                            <div className="card">
                                <div className="card-body">

                                    <div className="row border rounded-3 shadow-sm  mb-3">
                                        <div className="row align-items-center g-1 m-2">
                                            <div className="col-2 text-center">
                                                <Link className="text-dark" to="">
                                                    <span >
                                                        <img className="rounded-circle" src={userImg} height="55px" width="55px" alt='user' />
                                                    </span>
                                                </Link>
                                            </div>
                                            <div className="col-6 pe-4">
                                                <input className="form-control fs-6 my-2 py-3 rounded-3" type="text" name="PostsText"
                                                    id="PostsText" onChange={HandleForm} value={postInfo.PostsText} placeholder="What you like to share ?" />
                                            </div>
                                            <div className="col-1">
                                                <button onClick={ShareNewPost} className='btn btn-primary'>Share</button>
                                            </div>
                                        </div>
                                    </div>

                                    {loading ? <ClipLoader color={color} loading={loading} css={override} size={120} /> :
                                        <>
                                            {posts.map((post, index) => {
                                                return (
                                                    <div key={index} className="row border rounded-3 mb-2 p-1 shadow-sm">
                                                        <div className="row d-flex align-items-center mb-1">
                                                            <div className="col-6 justify-content-start d-flex">
                                                                <div className="me-1">
                                                                    <img className="rounded-circle me-1" src={userImg} width="20" height="20" alt='user' />
                                                                </div>
                                                                <Link to="/Profile"
                                                                    className="fs-6 text-success me-1 text-decoration-none">{post.Username}</Link>
                                                                <div className="font-weight-light text-danger">
                                                                    <span><small>{moment(post.CreatedAt).fromNow()}</small> </span>
                                                                </div>
                                                            </div>
                                                            <div className="col-6 justify-content-end d-flex">
                                                                {sessionStorage.getItem('Id') === post.FK_Users_Id ? <>
                                                                    <Link to="\Post\EditPost" className="text-decoration-none">
                                                                        <i className="fa fa-edit me-2 text-success"><span className="ms-1">Edit</span></i>
                                                                    </Link>
                                                                    <Link to="\Post\RemovePost" className="text-decoration-none">
                                                                        <i className="fa fa-trash me-2 text-danger"><span className="ms-1">Delete</span></i>
                                                                    </Link>
                                                                </> : ""}
                                                                <i className="fa fa-ellipsis-h me-1"></i>
                                                            </div>
                                                        </div>
                                                        {/* BODY */}
                                                        <div className="ms-3 mt-2 col-12">
                                                            <Link className="text-decoration-none text-black" to={"/post/" + post.Id}>
                                                                <div className="lh-sm text-dark text-wrap">
                                                                    {post.PostsText}
                                                                </div>
                                                            </Link>
                                                        </div>

                                                        <div className="row border-top mt-3 ms-0 py-1 d-flex align-items-center">
                                                            <div className="col-6 justify-content-start d-flex">
                                                                <Link to="" className="btn btn-outline-primary px-2 py-0">{post.LikeCount} <i className="fas fa-thumbs-up"></i></Link>
                                                                <Link to="" className="btn btn-outline-success px-2 py-0 ms-2">{post.CommentCount} <i className="fa fa-comment px-1 m-0"></i></Link>
                                                            </div>
                                                            <div className="col-6 justify-content-end d-flex">
                                                                <Link to="/Save/"><div className="mx-2"><i className="fa fa-heart me-1"></i></div></Link>
                                                                <div className="mx-2"><i className="fa fa-share me-1"></i></div>

                                                            </div>
                                                        </div>


                                                    </div>
                                                )
                                            })}









                                        </>
                                    }
                                </div>
                            </div>
                        </div>
                        <div className="col-4">
                            <div className="row border rounded-3 mb-2 ms-2 p-1 shadow-sm">
                                <div className="dashboard-nav bg-secondary rounded-3">
                                    <nav className="dashboard-nav-list">
                                        {sessionStorage.getItem('Type') === 'admin' && <>
                                            <Link to="/admin/home" className="dashboard-nav-item"><i className="fa fa-dashboard" /> Goto Admin Panel</Link>
                                        </>}
                                        <Link to={"/profile/" + sessionStorage.getItem('Id')} className="dashboard-nav-item"><i className="fa fa-user-circle" /> My Profile</Link>
                                        <Link to="/Follower/ShowFollowers" className="dashboard-nav-item"><i className="fa fa-users" /> Followers </Link>
                                        <Link to="/Follower/ShowFollowing" className="dashboard-nav-item"><i className="fa fa-user" /> Following </Link>
                                        <Link to="/Save/ShowSavePost" className="dashboard-nav-item"><i className="fa fa-heartbeat" /> Favourite Post </Link>
                                        <Link to="/post/MyPost" className="dashboard-nav-item"><i className="fa fa-book" /> My Posts </Link>
                                        <Link to="/password/change" className="dashboard-nav-item"><i className="fa fa-key" /> Change Password </Link>
                                        <Link to="/password/forget" className="dashboard-nav-item"><i className="fa fa-clock" /> Recovery Password </Link>
                                        <div className="nav-item-divider" />
                                        <Link to="/logout" className="dashboard-nav-item"><i className="fa fa-sign-out" /> Logout </Link>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </>}
        </>

    )



}

export default Home
