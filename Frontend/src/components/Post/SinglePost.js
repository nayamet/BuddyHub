import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { apiUrl } from '../../Config';
import { css } from "@emotion/react";
import { ClipLoader } from 'react-spinners';
import moment from 'moment';
import userImg from '../../images/user.png';
import { Link, useParams } from 'react-router-dom';

function AllPost() {

    let params = useParams();
    let [loading, setLoading] = useState(true);
    let [color, setColor] = useState("#ffffff");
    const override = css`
            display: block;
            margin: 0 auto;
            border-color: green;
            `;

    useEffect(() => {
        document.title = "BuddyHub - Posts";
        GetAllPost();
    }, []);

    let [post, setPost] = useState([]);
    let [comments, SetComments] = useState([]);
    var [postInfo, SetPostInfo] = useState({
        FK_Posts_Id: params.id,
        Text: "",
        FK_Users_Id: sessionStorage.getItem("Id")
    });


    const authAxios = axios.create({
        baseURL: apiUrl,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            Authorization: `${localStorage.getItem('Token')}`
        }
    })

    const GetAllPost = () => {
        authAxios.get(`${apiUrl}/post/${params.id}`).then(res => {
            console.log(res.data)
            setPost(res.data)
            GetAllComment(res.data.Id)
            setLoading(false);
        }).catch(err => {
            console.log(err)
        })
    }
    const GetAllComment = (cid) => {
        authAxios.get(`${apiUrl}/Comment/Post/${cid}`).then(res => {
            console.log(res.data)
            SetComments(res.data)
        }).catch(err => {
            console.log(err)
        })
    }
    const HandleForm = e => {
        SetPostInfo({
            ...postInfo,
            [e.target.name]: e.target.value,
        })
    }
    const ShareNewComment = () => {
        // console.log(postInfo);
        alert("Comment Added")
        axios.post(`${apiUrl}/Comment`, postInfo)
            .then(res => {
                // setLoading(true);
               // GetAllPost();
                console.log(res.data);
                SetPostInfo({
                    ...postInfo,
                    Text: "",
                })
                alert.success("Shared!!");

            })
            .catch(err => { console.log(err) })
    }

    return (
        <>
            {loading ? <ClipLoader color={color} loading={loading} css={override} size={120} /> :
                <>
                    <div className="container">
                        <div className="row d-flex justify-content-center">
                            <div className="col-8">
                                <div className="row border rounded-3 mb-2 p-1 shadow-sm">
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
                                            {sessionStorage.getItem('Id') === post.FK_User_Id ? <>
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

                                <div className="row rounded shadow mt-4">
                                    <div className="col-1">
                                        <img className="img-fluid img-responsive rounded-circle mr-2 m-2"
                                            src={userImg} width="45" height="45" alt='user' />
                                    </div>
                                    <div className="col-5">
                                        <form action="" method='POST'>
                                            <div className="d-flex">
                                                <div className="row w-100">
                                                    <div className="col-9">
                                                        <input type="text" name='Text' onChange={HandleForm} value={postInfo.PostsText} className="form-control mr-3 my-2"
                                                            placeholder="Add comment" />
                                                    </div>
                                                    <div className="col-3">
                                                        <button className="btn btn-primary my-2" 
                                                            onClick={ShareNewComment}>
                                                            Comment
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                    <hr className="text-muted my-2" />


                                    <h5>Comments:</h5>
                                    <hr className="text-muted my-2" />
                                    {comments.length > 0 ? comments.map((comment,i) => {
                                        return (
                                            <>

                                                <div key={i} className="row d-flex align-items-center mb-1">
                                                    <div className="col-6 justify-content-start d-flex">
                                                        <div className="me-1">
                                                            <img className="rounded-circle me-1" src={userImg} width="20" height="20" alt='user' />
                                                        </div>
                                                        <Link to="/Profile"
                                                            className="fs-6 text-success me-1 text-decoration-none">{post.Username}</Link>
                                                        <div className="font-weight-light text-danger">
                                                            <span><small>{moment(comment.CreatedAt).fromNow()}</small> </span>
                                                        </div>
                                                    </div>
                                                    <div className="col-6 justify-content-end d-flex">
                                                        <i className="fa fa-ellipsis-h me-1"></i>
                                                    </div>
                                                </div>
                                                {/* BODY */}
                                                <div className="ms-3 mt-2 mb-2 col-12">
                                                    <div className="lh-sm text-dark text-wrap">
                                                        {comment.Text}
                                                    </div>
                                                </div>

                                            </>
                                        )

                                    }) : <>

                                        <h6 className="ms-4">No Comments For This Post.</h6>

                                    </>}


                                </div>

                            </div>
                        </div>
                    </div>
                </>
            }
        </>
    )
}

export default AllPost
