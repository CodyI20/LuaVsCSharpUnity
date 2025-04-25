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
