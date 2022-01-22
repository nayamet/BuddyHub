import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { css } from "@emotion/react";
import { Link } from 'react-router-dom';
import { apiUrl } from '../../Config';
import AdminNavbar from './AdminNavbar'
import {ClipLoader } from 'react-spinners';

function AdminAllUser() {


    let [loading, setLoading] = useState(true);
    let [color, setColor] = useState("#ffffff");
    const override = css`
            display: block;
            margin: 0 auto;
            border-color: green;
            `;

    useEffect(() => {
        document.title = "BuddyHub - All User";
        GetProfileData();
    }, []);

    var [profileInfo, SetprofileInfo] = useState();

    const GetProfileData = () => {
        console.log("Getting Data");

        axios.get(`${apiUrl}/profile`)
            .then(res => {
                console.log(res.data)
                SetprofileInfo(res.data)
                setLoading(false);
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
                        {/* {profileInfo && profileInfo.map(user =>
                            <h5>{user.Name}</h5>
                        )} */}
                        {loading ? <ClipLoader color={color} loading={loading} css={override} size={120} /> :
                            <table className="table table-striped table-hover mt-2 mx-3">
                                <tbody>
                                    <tr className="table-dark">
                                        <th scope="col">#</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">Username</th>
                                        <th scope="col">Type</th>
                                        <th scope="col">Status</th>
                                        <th className="text-center" colSpan={3} scope="col">Action</th>
                                    </tr>
                                    {profileInfo && profileInfo.map((user, index) =>
                                        <tr key={index}>
                                            <td>{index + 1}</td>
                                            <td>{user.Name}</td>
                                            <td>{user.Username}</td>
                                            <td>{user.Type}</td>
                                            <td className="text-center">
                                                {user.Status === 1 && <><Link className="text-success" to="/User/ChangeStatus/@item.Username" data-bs-toggle="tooltip" data-bs-placement="top" title="Change Status">
                                                    <i className="fa fa-toggle-on" />
                                                </Link></>}
                                                {user.Status === 2 && <> <Link className="text-danger" to="/User/ChangeStatus/@item.Username" data-bs-toggle="tooltip" data-bs-placement="top" title="Change Status">
                                                    <i className="fa fa-toggle-off" />
                                                </Link></>}
                                            </td>
                                            <td className="text-center">
                                                <Link className="text-success text-decoration-none p-0 px-2 overlay" data-bs-toggle="tooltip" data-bs-placement="top" title="View" to={"/profile/" + user.FK_Users_Id}><i className="fa fa-eye" /></Link>
                                            </td>
                                            <td className="text-center">
                                                <Link className="text-danger text-decoration-none p-0 pe-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete" to={"/profile/delete" + user.FK_Users_Id}><i className="fa fa-trash" /></Link>
                                            </td>
                                        </tr>)}
                                </tbody>
                            </table>
                        }
                    </div>
                </div>
            </div>
        </>
    )
}

export default AdminAllUser
