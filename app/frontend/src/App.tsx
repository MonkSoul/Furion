import { Route, Routes } from "react-router-dom";
import Home from "./routes/home";
import Login from "./routes/login";

function App() {
  return (
    <Routes>
      <Route index element={<Home />} />
      <Route path="login" element={<Login />} />
    </Routes>
  );
}

export default App;
