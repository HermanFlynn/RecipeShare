//import { useState } from 'react';
//import './App.css';

//const API_BASE_URL = 'http://localhost:5154/recipe';

//function App() {
//    const [createRecipeData, setCreateRecipeData] = useState({
//        title: '',
//        ingredients: '',
//        cookingTimeMinutes: '',
//        dietaryTags: '',
//    });
//    const [readRecipeId, setReadRecipeId] = useState('');
//    const [filterTags, setFilterTags] = useState('');
//    const [updateRecipeId, setUpdateRecipeId] = useState('');
//    const [updateRecipeData, setUpdateRecipeData] = useState({
//        title: '',
//        ingredients: '',
//        cookingTimeMinutes: '',
//        dietaryTags: '',
//    });
//    const [deleteRecipeId, setDeleteRecipeId] = useState('');

//    const [response, setResponse] = useState(null);
//    const [error, setError] = useState(null);

//    const handleInputChange = (e, setState, dataKey = null) => {
//        const { name, value } = e.target;
//        if (dataKey) {
//            setState(prevState => ({ ...prevState, [dataKey]: { ...prevState[dataKey], [name]: value } }));
//        } else {
//            setState(value);
//        }
//    };

//    const parseStringToArray = (str) => {
//        return str.split(',').map(s => s.trim()).filter(s => s !== '');
//    };

//    const handleApiCall = async (endpoint, method, body = null, id = null) => {
//        setResponse(null);
//        setError(null);
//        let url = `${API_BASE_URL}/${endpoint}`;
//        if (id) {
//            url = `${API_BASE_URL}/${endpoint}?id=${id}`;
//        }

//        try {
//            const options = {
//                method: method,
//                headers: {
//                    'Content-Type': 'application/json',
//                },
//            };

//            if (body) {
//                options.body = JSON.stringify(body);
//            }

//            const res = await fetch(url, options);

//            if (!res.ok) {
//                const errorData = await res.json();
//                throw new Error(errorData.message || 'API call failed');
//            }

//            const data = await res.json();
//            setResponse(data);
//        } catch (err) {
//            setError(err.message);
//        }
//    };

//    const handleCreateRecipe = () => {
//        const data = {
//            title: createRecipeData.title,
//            ingredients: parseStringToArray(createRecipeData.ingredients),
//            cookingTimeMinutes: parseInt(createRecipeData.cookingTimeMinutes),
//            dietaryTags: parseStringToArray(createRecipeData.dietaryTags),
//        };
//        handleApiCall('create', 'POST', data);
//    };

//    const handleReadRecipe = () => {
//        handleApiCall('read', 'POST', null, readRecipeId);
//    };

//    const handleFilterRecipes = () => {
//        const tagsArray = parseStringToArray(filterTags);
//        handleApiCall('filterAsync', 'POST', tagsArray);
//    };

//    const handleReadAllRecipes = () => {
//        handleApiCall('readAll', 'POST');
//    };

//    const handleUpdateRecipe = () => {
//        const data = {
//            id: updateRecipeId,
//            title: updateRecipeData.title,
//            ingredients: parseStringToArray(updateRecipeData.ingredients),
//            cookingTimeMinutes: parseInt(updateRecipeData.cookingTimeMinutes),
//            dietaryTags: parseStringToArray(updateRecipeData.dietaryTags),
//        };
//        handleApiCall('update', 'POST', data, updateRecipeId);
//    };

//    const handleDeleteRecipe = () => {
//        handleApiCall('delete', 'POST', null, deleteRecipeId);
//    };

//    return (
//        <>
//            <h1>Recipe API Tester</h1>

//            {error && <div style={{ color: 'red', border: '1px solid red', padding: '10px', marginBottom: '10px' }}>Error: {error}</div>}
//            {response && <div style={{ background: '#e6ffe6', border: '1px solid green', padding: '10px', marginBottom: '10px', color: 'black' }}>Response: <pre>{JSON.stringify(response, null, 2)}</pre></div>}

//            <div className="card">
//                <h2>Create Recipe (POST /recipe/create)</h2>
//                <input type="text" name="title" placeholder="Title" value={createRecipeData.title} onChange={(e) => handleInputChange(e, setCreateRecipeData, 'createRecipeData')} /><br />
//                <textarea name="ingredients" placeholder="Ingredients (comma-separated)" value={createRecipeData.ingredients} onChange={(e) => handleInputChange(e, setCreateRecipeData, 'createRecipeData')} /><br />
//                <input type="number" name="cookingTimeMinutes" placeholder="Cooking Time (minutes)" value={createRecipeData.cookingTimeMinutes} onChange={(e) => handleInputChange(e, setCreateRecipeData, 'createRecipeData')} /><br />
//                <input type="text" name="dietaryTags" placeholder="Dietary Tags (comma-separated, e.g., Vegan,GlutenFree)" value={createRecipeData.dietaryTags} onChange={(e) => handleInputChange(e, setCreateRecipeData, 'createRecipeData')} /><br />
//                <button onClick={handleCreateRecipe}>Create Recipe</button>
//            </div>

//            <div className="card">
//                <h2>Read Recipe (POST /recipe/read)</h2>
//                <input type="text" placeholder="Recipe ID" value={readRecipeId} onChange={(e) => setReadRecipeId(e.target.value)} /><br />
//                <button onClick={handleReadRecipe}>Read Recipe</button>
//            </div>

//            <div className="card">
//                <h2>Filter Recipes (POST /recipe/filterAsync)</h2>
//                <textarea placeholder="Dietary Tags to Filter (comma-separated)" value={filterTags} onChange={(e) => setFilterTags(e.target.value)} /><br />
//                <button onClick={handleFilterRecipes}>Filter Recipes</button>
//            </div>

//            <div className="card">
//                <h2>Read All Recipes (POST /recipe/readAll)</h2>
//                <button onClick={handleReadAllRecipes}>Read All Recipes</button>
//            </div>

//            <div className="card">
//                <h2>Update Recipe (POST /recipe/update)</h2>
//                <input type="text" name="id" placeholder="Recipe ID to Update" value={updateRecipeId} onChange={(e) => setUpdateRecipeId(e.target.value)} /><br />
//                <input type="text" name="title" placeholder="Title" value={updateRecipeData.title} onChange={(e) => handleInputChange(e, setUpdateRecipeData, 'updateRecipeData')} /><br />
//                <textarea name="ingredients" placeholder="Ingredients (comma-separated)" value={updateRecipeData.ingredients} onChange={(e) => handleInputChange(e, setUpdateRecipeData, 'updateRecipeData')} /><br />
//                <input type="number" name="cookingTimeMinutes" placeholder="Cooking Time (minutes)" value={updateRecipeData.cookingTimeMinutes} onChange={(e) => handleInputChange(e, setUpdateRecipeData, 'updateRecipeData')} /><br />
//                <input type="text" name="dietaryTags" placeholder="Dietary Tags (comma-separated)" value={updateRecipeData.dietaryTags} onChange={(e) => handleInputChange(e, setUpdateRecipeData, 'updateRecipeData')} /><br />
//                <button onClick={handleUpdateRecipe}>Update Recipe</button>
//            </div>

//            <div className="card">
//                <h2>Delete Recipe (POST /recipe/delete)</h2>
//                <input type="text" placeholder="Recipe ID to Delete" value={deleteRecipeId} onChange={(e) => setDeleteRecipeId(e.target.value)} /><br />
//                <button onClick={handleDeleteRecipe}>Delete Recipe</button>
//            </div>
//        </>
//    );
//}

//export default App;


import { useState } from 'react';
import './App.css';
//import { useEffect } from 'react';

const API_BASE_URL = 'http://localhost:5154/recipe';

const VALID_DIETARY_TAGS = [
    "None", "Vegetarian", "Vegan", "Pescatarian", "GlutenFree", "DairyFree", "NutFree", "Halal", "Kosher",
    "LowCarb", "LowFat", "HighProtein", "LowSodium", "LowSugar", "Organic", "NonGMO", "Whole30", "Paleo",
    "Keto", "FODMAPFriendly", "DiabeticFriendly", "HeartHealthy", "AntiInflammatory", "Raw", "LowCholesterol",
    "HighFiber", "LowCalorie", "IntermittentFasting", "AllergenFriendly", "SugarFree", "LowHistamine",
    "LowOxalate", "LowFODMAP", "LowPurine", "LowPhosphorus", "LowPotassium", "LowMagnesium", "LowCalcium",
    "LowIron", "LowZinc", "LowCopper", "LowSelenium", "LowManganese", "LowIodine", "LowFluoride", "LowChloride"
];

const DIETARY_TAG_NAME_TO_VALUE = {
    "None": 0, "Vegetarian": 1, "Vegan": 2, "Pescatarian": 3, "GlutenFree": 4, "DairyFree": 5, "NutFree": 6, "Halal": 7, "Kosher": 8,
    "LowCarb": 9, "LowFat": 10, "HighProtein": 11, "LowSodium": 12, "LowSugar": 13, "Organic": 14, "NonGMO": 15, "Whole30": 16, "Paleo": 17,
    "Keto": 18, "FODMAPFriendly": 19, "DiabeticFriendly": 20, "HeartHealthy": 21, "AntiInflammatory": 22, "Raw": 23, "LowCholesterol": 24,
    "HighFiber": 25, "LowCalorie": 26, "IntermittentFasting": 27, "AllergenFriendly": 28, "SugarFree": 29, "LowHistamine": 30,
    "LowOxalate": 31, "LowFODMAP": 32, "LowPurine": 33, "LowPhosphorus": 34, "LowPotassium": 35, "LowMagnesium": 36, "LowCalcium": 37,
    "LowIron": 38, "LowZinc": 39, "LowCopper": 40, "LowSelenium": 41, "LowManganese": 42, "LowIodine": 43, "LowFluoride": 44, "LowChloride": 45
};

const DIETARY_TAG_VALUE_TO_NAME = Object.fromEntries(
    Object.entries(DIETARY_TAG_NAME_TO_VALUE).map(([name, value]) => [value, name])
);

function App() {
    const [createRecipeData, setCreateRecipeData] = useState({
        title: '',
        ingredients: '',
        cookingTimeMinutes: '',
        dietaryTags: '',
    });
    const [readRecipeId, setReadRecipeId] = useState('');
    const [filterTags, setFilterTags] = useState('');
    const [updateRecipeId, setUpdateRecipeId] = useState('');
    const [updateRecipeData, setUpdateRecipeData] = useState({
        title: '',
        ingredients: '',
        cookingTimeMinutes: '',
        dietaryTags: '',
    });
    const [deleteRecipeId, setDeleteRecipeId] = useState('');

    const [response, setResponse] = useState(null);
    const [error, setError] = useState(null);

    const handleFormChange = (e, formStateSetter) => {
        const { name, value } = e.target;
        formStateSetter(prevState => ({ ...prevState, [name]: value }));
    };

    const parseAndValidateTags = (tagString) => {
        const rawTags = tagString.split(',').map(s => s.trim()).filter(s => s !== '');
        const validTagsAsStrings = [];
        const invalidInputs = [];

        for (const tag of rawTags) {
            const tagAsNum = parseInt(tag, 10);
            if (!isNaN(tagAsNum) && Object.prototype.hasOwnProperty.call(DIETARY_TAG_VALUE_TO_NAME, tagAsNum)) {
                validTagsAsStrings.push(DIETARY_TAG_VALUE_TO_NAME[tagAsNum]);
            }
            else if (Object.prototype.hasOwnProperty.call(DIETARY_TAG_NAME_TO_VALUE, tag)) {
                validTagsAsStrings.push(tag);
            }
            else {
                invalidInputs.push(tag);
            }
        }

        if (invalidInputs.length > 0) {
            alert(`Warning: The following dietary tags are invalid and will be ignored: ${invalidInputs.join(', ')}. Please use valid names or numbers (0-45).`);
        }
        return validTagsAsStrings;
    };

    const parseIngredients = (ingredientsString) => {
        return ingredientsString.split(',').map(s => s.trim()).filter(s => s !== '');
    };

    const handleApiCall = async (endpoint, method, body = null, id = null) => {
        setResponse(null);
        setError(null);
        let url = `${API_BASE_URL}/${endpoint}`;

        if (id && (endpoint === 'read' || endpoint === 'delete')) {
            url = `${API_BASE_URL}/${endpoint}?id=${id}`;
        }

        if (id && endpoint === 'update') {
            url = `${API_BASE_URL}/${endpoint}?id=${id}`;
        }


        try {
            const options = {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                },
            };

            if (body) {
                options.body = JSON.stringify(body);
            }

            const res = await fetch(url, options);

            if (!res.ok) {
                const errorData = await res.json();
                throw new Error(errorData.message || 'API call failed');
            }

            const data = await res.json();
            setResponse(data);
        } catch (err) {
            setError(err.message);
        }
    };

    const handleCreateRecipe = () => {
        const data = {
            title: createRecipeData.title,
            ingredients: parseIngredients(createRecipeData.ingredients),
            cookingTimeMinutes: parseInt(createRecipeData.cookingTimeMinutes),
            dietaryTags: parseAndValidateTags(createRecipeData.dietaryTags),
        };
        handleApiCall('create', 'POST', data);
    };

    const handleReadRecipe = () => {
        handleApiCall('read', 'POST', null, readRecipeId);
    };

    const handleFilterRecipes = () => {
        const tagsArray = parseAndValidateTags(filterTags);
        handleApiCall('filterAsync', 'POST', tagsArray);
    };

    const handleReadAllRecipes = () => {
        handleApiCall('readAll', 'POST');
    };

    const handleUpdateRecipe = () => {
        const data = {
            id: updateRecipeId,
            title: updateRecipeData.title,
            ingredients: parseIngredients(updateRecipeData.ingredients),
            cookingTimeMinutes: parseInt(updateRecipeData.cookingTimeMinutes),
            dietaryTags: parseAndValidateTags(updateRecipeData.dietaryTags),
        };
        handleApiCall('update', 'POST', data, updateRecipeId);
    };

    const handleDeleteRecipe = () => {
        handleApiCall('delete', 'POST', null, deleteRecipeId);
    };

    return (
        <div className="container">
            <h1>Recipe API Tester</h1>

            {error && <div className="message error">Error: {error}</div>}
            {response && <div className="message success">Response: <pre>{JSON.stringify(response, null, 2)}</pre></div>}

            <div className="card">
                <h2>Create Recipe (POST /recipe/create)</h2>
                <input type="text" name="title" placeholder="Title" value={createRecipeData.title} onChange={(e) => handleFormChange(e, setCreateRecipeData)} /><br />
                <textarea name="ingredients" placeholder="Ingredients (comma-separated)" value={createRecipeData.ingredients} onChange={(e) => handleFormChange(e, setCreateRecipeData)} /><br />
                <input type="number" name="cookingTimeMinutes" placeholder="Cooking Time (minutes)" min="0" value={createRecipeData.cookingTimeMinutes} onChange={(e) => handleFormChange(e, setCreateRecipeData)} /><br />
                <input type="text" name="dietaryTags" placeholder="Dietary Tags (comma-separated, e.g., Vegan,GlutenFree)" value={createRecipeData.dietaryTags} onChange={(e) => handleFormChange(e, setCreateRecipeData)} /><br />
                <button onClick={handleCreateRecipe}>Create Recipe</button>
            </div>

            <div className="card">
                <h2>Read Recipe (POST /recipe/read)</h2>
                <input type="text" placeholder="Recipe ID" value={readRecipeId} onChange={(e) => setReadRecipeId(e.target.value)} /><br />
                <button onClick={handleReadRecipe}>Read Recipe</button>
            </div>

            <div className="card">
                <h2>Filter Recipes (POST /recipe/filterAsync)</h2>
                <textarea placeholder="Dietary Tags to Filter (comma-separated)" value={filterTags} onChange={(e) => setFilterTags(e.target.value)} /><br />
                <button onClick={handleFilterRecipes}>Filter Recipes</button>
            </div>

            <div className="card">
                <h2>Read All Recipes (POST /recipe/readAll)</h2>
                <button onClick={handleReadAllRecipes}>Read All Recipes</button>
            </div>

            <div className="card">
                <h2>Update Recipe (POST /recipe/update)</h2>
                <input type="text" name="id" placeholder="Recipe ID to Update" value={updateRecipeId} onChange={(e) => setUpdateRecipeId(e.target.value)} /><br />
                <input type="text" name="title" placeholder="Title" value={updateRecipeData.title} onChange={(e) => handleFormChange(e, setUpdateRecipeData)} /><br />
                <textarea name="ingredients" placeholder="Ingredients (comma-separated)" value={updateRecipeData.ingredients} onChange={(e) => handleFormChange(e, setUpdateRecipeData)} /><br />
                <input type="number" name="cookingTimeMinutes" placeholder="Cooking Time (minutes)" min="0" value={updateRecipeData.cookingTimeMinutes} onChange={(e) => handleFormChange(e, setUpdateRecipeData)} /><br />
                <input type="text" name="dietaryTags" placeholder="Dietary Tags (comma-separated)" value={updateRecipeData.dietaryTags} onChange={(e) => handleFormChange(e, setUpdateRecipeData)} /><br />
                <button onClick={handleUpdateRecipe}>Update Recipe</button>
            </div>

            <div className="card">
                <h2>Delete Recipe (POST /recipe/delete)</h2>
                <input type="text" placeholder="Recipe ID to Delete" value={deleteRecipeId} onChange={(e) => setDeleteRecipeId(e.target.value)} /><br />
                <button onClick={handleDeleteRecipe}>Delete Recipe</button>
            </div>

            <div className="card">
                <h2>All Valid Dietary Tags</h2>
                <p style={{ wordBreak: 'break-all' }}>{VALID_DIETARY_TAGS.join(', ')}</p>
            </div>
        </div>
    );
}

export default App;