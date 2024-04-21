/* eslint-disable prettier/prettier */
import { FC } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import PageLayout from './components/PageLayout';
import Profile from './views/Profile';
import Recommendations from './views/Recommendations';
import RegistrationPage from './views/auth/RegistrationPage';
import AuthLayout from './components/AuthLayout';
import Login from './views/auth/Login';
import ConfirmEmail from './views/auth/ConfirmEmail';
import Add from './views/Add';

const App: FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          element={<AuthLayout />}
          path='/auth'
        >
          <Route
            element={<RegistrationPage />}
            path='registration'
          />
          <Route
            element={<Login />}
            path='login'
          />
          <Route
            element={<ConfirmEmail />}
            path='confirm-email'
          />
        </Route>
        <Route
          element={<PageLayout />}
          path='/'
        >
          <Route
            element={<Recommendations />}
            path='recommendation'
          />
          <Route
            element={<Profile />}
            path='profile/'
          />
          <Route
            element={<Profile />}
            path='profile/:id'
          />
          <Route
            element={<h1>Search</h1>}
            path='search'
          />
          <Route
            element={<Add />}
            path='/add'
          />
        </Route>

        <Route
          element={<h1>Not found</h1>}
          path='*'
        />
      </Routes>
    </BrowserRouter>
  )
};

export default App;
