import { Navigate, Route, Routes, useLocation } from "react-router-dom";
import Home from "./pages/home";
import Login from "./pages/login";
import AuthProvider from "./shared/contexts/AuthProvider";
import useAuth from "./shared/hooks/useAuth";

function App() {
  return (
    <AuthProvider>
      <Routes>
        <Route
          index
          element={
            <Private>
              <Home />
            </Private>
          }
        />
        <Route path="login" element={<Login />} />
      </Routes>
    </AuthProvider>
  );
}

function Private({ children }: { children: JSX.Element }) {
  let auth = useAuth();
  let location = useLocation();

  if (!auth.user) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
}

export default App;
