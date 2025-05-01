function update_health()
    math.randomseed(42)
    local health = 100
    for i = 1, 1000 do
        local damage = math.random(5, 20)
        local heal = math.random(1, 30)
        health = health - damage
        health = math.min(100, health + heal)
        if health < 10 then
            set_color("red")
        else
            set_color("green")
        end
        set_fill(health / 100)
    end
end

function display_text()
    for i=1, 1000 do
        set_text("Hello from LUA!")
    end
end

function nested_loop_test()
    local result = 0

    for i = 0, 999 do
        for j = 0, 199 do
            -- Basic arithmetic operations
            local addition = i + j
            local multiplication = i * j
            local division = (j ~= 0) and (i / j) or 0
            local subtraction = i - j

            -- Trigonometric operations
            local sine = math.sin(math.rad(i))
            local cosine = math.cos(math.rad(j))
            local tangent = math.tan(math.rad(i + j))

            -- Exponential and logarithmic operations
            local exponential = math.exp(i % 10)
            local logarithm = (i + j > 0) and math.log(i + j) or 0

            -- Combine results
            result = result + addition + multiplication + division + subtraction + sine + cosine + tangent + exponential + logarithm
        end
    end

    set_text("The LUA result is: " .. tostring(result))
end

function sort_search_test()
    math.randomseed(os.time()) -- Seed for random number generation
    local result = 0

    for i = 1, 1000 do
        -- Generate a table of 500 random integers
        local numbers = {}
        for j = 1, 500 do
            table.insert(numbers, math.random(1, 1000)) -- Random numbers between 1 and 1000
        end

        -- Sort the table
        table.sort(numbers)

        -- Perform an aggregation (e.g., compute the sum of the table)
        local sum = 0
        for _, num in ipairs(numbers) do
            sum = sum + num
        end

        -- Accumulate the result
        result = result + sum
    end

    set_text("The LUA result is: " .. tostring(result))
end

function allocate_and_discard_test()
    local result = 0

    for i = 1, 1000 do
        -- Allocate a new table
        local list = {}

        -- Fill the table with 1000 integers
        for j = 1, 1000 do
            list[j] = j
        end

        -- Discard the table by setting it to nil
        list = nil

        -- Accumulate a dummy result to prevent optimization
        result = result + 0 -- No equivalent to list.Count in Lua
    end

    set_text("The LUA result is: " .. tostring(result))
end


function dynamic_ui_element_generation()
    local parent = get_ui_parent() -- Retrieve the parent Transform
    
        for i = 1, 1000 do
            -- Instantiate a new UI element
            local uiElement = instantiate_ui_prefab()
            local parentRect = parent:GetComponent("RectTransform")
    
            if parentRect ~= nil then
                -- Generate random positions within the parent's bounds
                local x = math.random(parentRect.rect.xMin, parentRect.rect.xMax)
                local y = math.random(parentRect.rect.yMin, parentRect.rect.yMax)
    
                -- Clamp the position within the parent's bounds
                local clampedX = math.max(parentRect.rect.xMin, math.min(x, parentRect.rect.xMax))
                local clampedY = math.max(parentRect.rect.yMin, math.min(y, parentRect.rect.yMax))
                set_position(uiElement, clampedX, clampedY)
            else
                print("Parent does not have a RectTransform. Positioning may not be constrained.")
    
                -- Generate random positions without bounds
                local x = math.random(-500, 500)
                local y = math.random(-500, 500)
                set_position(uiElement, x, y)
            end
    
            -- Optionally destroy some elements after a few iterations
            if i % 10 == 0 then
                destroy_ui_element(uiElement)
            end
        end
end