import React from 'react'
import { NavLink } from 'react-router-dom'

function AdminNavbar() {
    return (
        <>
            <div className="row border rounded-3 mb-2 ms-2 p-1 shadow-sm">
                <div className="dashboard-nav bg-secondary rounded-3">
                    <nav className="dashboard-nav-list">
                        <NavLink to="/admin/home" className={c => c.isActive ? "dashboard-nav-item active" : "dashboard-nav-item"}><i className="fa fa-user-circle" /> Dashboard</NavLink>
                        <NavLink to="/admin/all-user" className={c => c.isActive ? "dashboard-nav-item active" : "dashboard-nav-item"}><i className="fa fa-users" /> All User </NavLink>
                        <NavLink to="/admin/all-post" className={c => c.isActive ? "dashboard-nav-item active" : "dashboard-nav-item"}><i className="fa fa-hacker-news" /> All Post </NavLink>
                        <NavLink to={"/profile/" + sessionStorage.getItem('Id')} className={c => c.isActive ? "dashboard-nav-item active" : "dashboard-nav-item"}><i className="fa fa-user" /> Own Profile </NavLink>
                        <div className="nav-item-divider" />
                        <NavLink to="/Logout" className="dashboard-nav-item"><i className="fa fa-sign-out" /> Logout </NavLink>
                    </nav>
                </div>
            </div>

        </>
    )
}

export default AdminNavbar
