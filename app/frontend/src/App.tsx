import { Route, Routes } from "react-router-dom";
import Home from "./pages/home";
import Login from "./pages/login";

function App() {
  return (
    <Routes>
      <Route index element={<Home />} />
      <Route path="login" element={<Login />} />
    </Routes>
  );
}

export default App;
