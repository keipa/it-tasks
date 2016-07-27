module Damage
  def self.execute record, probability
    mistakes_done = 0
    while mistakes_done < probability
      case Random.rand 5
  	  when 0
  	  	record = permutation_of_two_adjacent_digits record
  	  when 1
  	  	record = replacement_of_one_digit_to_another record
  	  when 2
  	  	record = remove_letter record
  	  when 3
  	  	record = double_letter record
      when 4
        record = permutation_of_two_letters record
      when 5
        record = insert_letter record
      end
      mistakes_done += 1

    end

  	record
  end


  def self.permutation_of_two_adjacent_digits record
    if record.size >= 2
  		index = 0
      array_record = record.split(//).to_a
    	(record.size - 1).times do
        if array_record[index].match(/\d/) && array_record[index + 1].match(/\d/)
          record[index + 1], record[index] = record[index], record[index + 1]
          break
        end
    		index += 1
    	end
    end
  	record
  end

  def self.replacement_of_one_digit_to_another record

    if record.size >= 2
      index = 0
      array_record = record.split(//).to_a
      (record.size - 1).times do
        if array_record[index].match(/\d/)
          record[index] = "#{rand(10)}"
          break
        end
        index += 1
      end
    end
    record
  end

  def self.remove_letter record


    record[rand(record.size - 1)] = ""
    record  end

  def self.insert_letter record

      randletter = record[rand(15)]
      record[rand(record.size - 1)] += randletter
      record
  end


  def self.double_letter record

    a= rand(record.size-1)
    record[a] = record[a]+record[a]
    record
  end

  def self.permutation_of_two_letters record
    if record.size >= 2
      index = rand(10)
      array_record = record.split(//).to_a
      (record.size - 1).times do
        if array_record[index].match(/\D/) && array_record[index + 1].match(/\D/)
          record[index + 1], record[index] = record[index], record[index + 1]
          break
        end
        index += 1
        if index > record.size-1
          break
        end
      end
    end
    record
  end
 end

