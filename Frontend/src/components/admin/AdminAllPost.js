import axios from 'axios';
import moment from 'moment';
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';
import { css } from "@emotion/react";

import { apiUrl } from '../../Config';
import AdminNavbar from './AdminNavbar'
import { ScaleLoader, ClipLoader, RingLoader, SyncLoader } from 'react-spinners';



function AdminAllPost() {

    moment.locale('en');
    let [loading, setLoading] = useState(true);
    let [color, setColor] = useState("#ffffff");
    const override = css`
            display: block;
            margin: 0 auto;
            border-color: green;
            `;

    useEffect(() => {
        document.title = "BuddyHub - Profile";
        GetPostData();
    }, []);

    var [postInfo, SetPostInfo] = useState();

    const GetPostData = () => {
        console.log("Getting Data");

        axios.get(`${apiUrl}/Post`)
            .then(res => {
                SetPostInfo(res.data)
                setLoading(false);
                console.log(res.data);
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
                        {/* {postInfo && postInfo.map(post => 
                            <h5></h5>
                        )} */}
                        {loading ? <ClipLoader color={color} loading={loading} css={override} size={120} /> :
                            <table className="table table-striped table-hover mt-2 mx-3">
                                <tbody><tr className="table-dark">
                                    <th scope="col">#</th>
                                    <th scope="col">Post ID</th>
                                    <th scope="col">Username</th>
                                    <th scope="col">CreatedAt</th>
                                    <th scope="col">Status</th>
                                    <th className="text-center" colSpan={3} scope="col">Action</th>
                                </tr>
                                    {postInfo && postInfo.map((post, index) =>
                                        <tr key={index}>
                                            <td>{index + 1}</td>
                                            <td>{post.Id}</td>
                                            <td>{post.Username}</td>
                                            <td>{moment(post.CreatedAt).format('DD-MMM-YYYY')}</td>
                                            <td className="text-center">
                                                {post.Status === 0 && <><Link className="text-success" to="/Post/ChangeStatus/@item.PostId" data-bs-toggle="tooltip" data-bs-placement="top" title="Change Status">
                                                    <i className="fa fa-toggle-on" />
                                                </Link></>}
                                                {post.Status === 1 && <>  <Link className="text-danger" to="/Post/ChangeStatus/@item.PostId" data-bs-toggle="tooltip" data-bs-placement="top" title="Change Status">
                                                    <i className="fa fa-toggle-off" />
                                                </Link></>}
                                            </td>
                                            <td className="text-center">
                                                <Link className="text-success text-decoration-none p-0 px-2 overlay" data-bs-toggle="tooltip" data-bs-placement="top" title="View" to={"/post/" + post.Id}><i className="fa fa-eye" /></Link>
                                            </td>
                                            <td className="text-center">
                                                <Link className="text-danger text-decoration-none p-0 pe-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" to="/Post/AdminRemovePost/@item.PostId"><i className="fa fa-trash" /></Link>
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        }
                    </div>
                </div>
            </div>
        </>
    )
}

export default AdminAllPost
