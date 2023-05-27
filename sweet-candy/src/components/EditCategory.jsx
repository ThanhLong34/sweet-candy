import React, { useState, useRef } from "react";

const EditCategory = () => {
	const nameRef = useRef(null);
	const showOnMenuRef = useRef(null);
	const messageRef = useRef(null);

	function handleSubmit(e) {
		e.preventDefault();

		const category = {
			name: nameRef.current.value,
			showOnMenu: showOnMenuRef.current.checked,
		};

		messageRef.current.innerText = "Đang gửi dữ liệu...";

		// Submit
		fetch("https://localhost:7095/categories", {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify(category),
		})
			.then((response) => response.json())
			.then((response) => (messageRef.current.innerText = ""))
			.catch((err) => {
				messageRef.current.innerText = "Đã có lỗi xảy ra";
			});
	}

	return (
		<div>
			<h1>Add New Category</h1>
			<form onSubmit={handleSubmit}>
				<label>
					{" "}
					Name:
					<input ref={nameRef} type="text" />
				</label>

				<label>
					{" "}
					Show On Menu:
					<input ref={showOnMenuRef} type="checkbox" />
				</label>

				<button type="submit">Submit</button>
				<p ref={messageRef}></p>
			</form>
		</div>
	);
};

export default EditCategory;
