import React, { useState, useEffect } from "react";

const CandyList = () => {
	const [candies, setCandies] = useState([]);
	const [errorMessage, setErrorMessage] = useState("");

	useEffect(() => {
		fetch(`https://localhost:7095/candies?Name=abc&MinPrice=fabwfawf`)
			.then((response) => response.json())
			.then((data) => {
				if (data.length === 0) {
					setErrorMessage("Không có dữ liệu");
				}
				setCandies(data);
			})
			.catch((err) => {
				setErrorMessage("Không thể tải dữ liệu");
			});
	}, []);

	if (errorMessage) {
		return <p>{errorMessage}</p>;
	}

	return (
		<div className="product-list">
			{candies.length > 0 ? (
				candies.map((item) => (
					<div className="product-item" key={item.id}>
						<h1>{item.name}</h1>
						<p>$ {item.price}</p>
					</div>
				))
			) : (
				<p>Đang tải dữ liệu</p>
			)}
		</div>
	);
};

export default CandyList;
