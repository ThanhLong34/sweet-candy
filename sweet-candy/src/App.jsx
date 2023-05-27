import "./App.css";
import CandyList from "./components/CandyList";
import EditCategory from "./components/EditCategory";

function App() {
	return (
		<div className="App">
			<h1>Sweet Candy</h1>
			<CandyList />

			<EditCategory />
		</div>
	);
}

export default App;
