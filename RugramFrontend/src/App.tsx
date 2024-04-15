import { FC } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import PageLayout from './components/PageLayout';
import Profile from './views/Profile';
import Recommendations from './views/Recommendations';
import Registration from './views/auth/Registration';
import AuthLayout from './components/AuthLayout';
import Login from './views/auth/Login';

const App : FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          element={<AuthLayout />}
          path='/'
        >
          <Route path='registration' element={<Registration />} />
          <Route path='login' element={<Login />} />
        </Route>
        <Route
          element={<PageLayout />}
        >
          <Route path='recommendation' element={<Recommendations />} />
          <Route path='profile/' element={<Profile />} />
          <Route path='profile/:id' element={<Profile />} />
          <Route path='search' element={<h1>Search</h1>} />
        </Route>

        <Route path='*' element={<h1>Not found</h1>} />
      </Routes>
    </BrowserRouter>
  )
};

export default App;
