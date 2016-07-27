# encoding: utf-8
module Ryba
  class Name
    class <<self
      def gender
        male = Kernel.rand(100) < 50
        yield(male) if block_given?
        male
      end




      def first_nameBY(male = nil)
        pick_with_gender(male, Data::MaleNamesBY, Data::FemaleNamesBY)
      end

      def middle_nameBY(male = nil)
        pick_with_gender(male, Data::MaleMiddleNamesBY, Data::FemaleMiddleNamesBY)
      end

      def family_nameBY(male = nil)
        male = gender if male.nil?

        famname = Ryba.pick(Data::FamilyNamesBY)

        if famname.is_a? Array
          famname[0] + famname[male ? 1 : 2]
        else
          male ? famname : famname
        end
      end

      def full_nameBY(male = nil)
        male = gender if male.nil?

        "#{family_nameBY(male)} #{first_nameBY(male)} #{middle_nameBY(male)}"
      end





      def first_name(male = nil)
        pick_with_gender(male, Data::MaleNames, Data::FemaleNames)
      end

      def middle_name(male = nil)
        pick_with_gender(male, Data::MaleMiddleNames, Data::FemaleMiddleNames)
      end

      def family_name(male = nil)
        male = gender if male.nil?

        famname = Ryba.pick(Data::FamilyNames)

        if famname.is_a? Array
          famname[0] + famname[male ? 1 : 2]
        else
          male ? famname : famname + 'а'
        end
      end

      def full_name(male = nil)
        male = gender if male.nil?

        "#{family_name(male)} #{first_name(male)} #{middle_name(male)}"
      end

      private

      def pick_with_gender(male, male_ary, female_ary)
        male = gender if male.nil?
        Ryba.pick(male ? male_ary : female_ary)
      end
    end
  end
end
