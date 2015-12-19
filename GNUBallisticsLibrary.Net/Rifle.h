#pragma once

using namespace System;
using namespace Sharp::Ballistics::Abstractions;
using namespace UnitsNet;
using namespace System::Collections::Generic;

namespace GNUBallisticsLibrary
{

#define ZERO_RANGE 100
	public ref class Rifle : IRifle
	{
	private:
		RifleInfo^ rifleInfo;
		Scope^ scopeInfo;
		Cartridge^ ammoInfo;

		double zeroAngle;

		String^ id;
	public:
		Rifle()
		{
		}

		Rifle(RifleInfo^ rifleInfo, Scope^ scopeInfo, Cartridge^ ammoInfo)
		{
			Initialize(rifleInfo, scopeInfo, ammoInfo);
		}

		void Initialize(RifleInfo^ rifleInfo, Scope^ scopeInfo, Cartridge^ ammoInfo)
		{
			if (rifleInfo == nullptr)
				throw gcnew System::ArgumentNullException("rifleInfo");

			if (rifleInfo->ZeroingConditions == nullptr)
				throw gcnew System::ArgumentNullException("rifleInfo.ZeroingConditions");

			if (scopeInfo == nullptr)
				throw gcnew System::ArgumentNullException("scopeInfo");
			if (ammoInfo == nullptr)
				throw gcnew System::ArgumentNullException("ammoInfo");

			this->rifleInfo = rifleInfo;
			this->scopeInfo = scopeInfo;
			this->ammoInfo = ammoInfo;

			zeroAngle = ZeroAngle((int)ammoInfo->DragFunction,
				AtmCorrect(ammoInfo->BC,
					rifleInfo->ZeroingConditions->Altitude.Feet,
					rifleInfo->ZeroingConditions->Barometer.Psi * 2.03602, //convert to Hg
					rifleInfo->ZeroingConditions->Temperature.DegreesFahrenheit,
					rifleInfo->ZeroingConditions->RelativeHumidity),
				ammoInfo->MuzzleVelocity.FeetPerSecond,
				scopeInfo->Height.Inches,
				scopeInfo->ZeroDistance.Yards, 0);
		}

		virtual BallisticSolution^ Solve(
			double shootingAngle,
			Speed windSpeed,
			double windAngle,
			Length range,
			WeatherCondition^ atmInfo,
			ShotLocationInfo^ shotLocationInfo);

		virtual IEnumerable<BallisticSolution^>^ SolveMultiple(double shootingAngle,
			Speed windSpeed,
			double windAngle,
			IEnumerable<Length>^ ranges,
			WeatherCondition^ atmInfo,
			ShotLocationInfo^ shotLocationInfo);

		virtual property String^ Name
		{
			String^ get()
			{
				if (rifleInfo == nullptr)
					return nullptr;
				return rifleInfo->Name;
			}
			void set(String^ val)
			{
				rifleInfo->Name = val;
			}
		}

		virtual property WeatherCondition^ ZeroingConditions
		{
			WeatherCondition^ get()
			{
				if (rifleInfo == nullptr)
					return nullptr;
				return rifleInfo->ZeroingConditions;
			}
			void set(WeatherCondition^ val)
			{
				rifleInfo->ZeroingConditions = val;
			}
		}

		virtual property Cartridge^ Ammo
		{
			Cartridge^ get()
			{
				return ammoInfo;
			}

			void set(Cartridge^ val)
			{
				ammoInfo = val;
			}
		}

		virtual property Scope^ MountedScope
		{
			Scope^ get()
			{
				return scopeInfo;
			}
			void set(Scope^ val)
			{
				scopeInfo = val;
			}
		}

	public:
		virtual property String^ Id
		{
			String^ get()
			{
				return id;
			}
		private:
			void set(String^ val)
			{
				id = val;
			}
		}
	};
}