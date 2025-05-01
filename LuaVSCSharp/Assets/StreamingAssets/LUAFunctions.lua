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
